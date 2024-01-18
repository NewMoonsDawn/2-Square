using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScreenshotCamera : MonoBehaviour
{
    public RenderTexture tabletRender;
    public Transform raycastShootPoint;
    public LayerMask relevantMask;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

#if UNITY_EDITOR
        // Empty out screenshots folder when the game starts
        string[] files = System.IO.Directory.GetFiles(Application.dataPath + "/CameraScreenshots/");

        if (files.Length > 0)
        {
            foreach (string file in files)
            {
                // FileUtil exists only in the editor
                FileUtil.DeleteFileOrDirectory(file);
                print("Deleting" + file);
            }
        }
#endif
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) { StartCoroutine(TakeScreenshot()); }
    }

    private IEnumerator TakeScreenshot()
    {
        // Raycast
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(raycastShootPoint.position, raycastShootPoint.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, relevantMask))
        {
            string screenshotName = hit.transform.gameObject.name + ".png";

            // If folder contains a screenshot with this name - screenshot is already taken
            string[] files = System.IO.Directory.GetFiles(Application.dataPath + "/CameraScreenshots/");

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    if (file.Contains(screenshotName))
                    {
                        print("Already taken a screenshot of " + screenshotName);
                        yield break;
                    }
                }
            }


            yield return new WaitForEndOfFrame();

            RenderTexture rt = new RenderTexture(863, 444, 24);
            cam.targetTexture = rt;
            Texture2D screenShot = new Texture2D(863, 444, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, 863, 444), 0, 0);
            cam.targetTexture = tabletRender;
            RenderTexture.active = tabletRender; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = Application.dataPath + "/CameraScreenshots/" + screenshotName;
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
        }
    }
}
