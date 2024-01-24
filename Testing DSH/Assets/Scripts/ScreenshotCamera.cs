using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScreenshotCamera : MonoBehaviour
{
    public TaskManager taskManager;


    public int screenshotsTaken;

    public bool interactable;
    public MeshRenderer tablet;
    public RenderTexture tabletRender;
    public Material offMaterial;
    public Material renderMaterial;
    public Transform raycastShootPoint;
    public GameObject canvas;
    public LayerMask relevantMask;
    public bool isOn;
    public AudioSource playerSource;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

#if UNITY_EDITOR
        // Empty out screenshots folder when the game starts
        string[] files = System.IO.Directory.GetFiles(Application.dataPath + "/Resources/CameraScreenshots/");

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
        if (!interactable) return;
        //if (isOn && Input.GetButtonDown("XRI_Left_SecondaryButton")) { StartCoroutine(TakeScreenshot()); }

        // Change input to VR input
        if (Input.GetButtonDown("VR")) { StartCoroutine(TakeScreenshot()); }
        if (Input.GetKeyDown(KeyCode.T)) { StartCoroutine(TakeScreenshot()); }
    }

    private IEnumerator TakeScreenshot()
    {
        // Raycast
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(raycastShootPoint.position, raycastShootPoint.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) //, relevantMask
        {
            string screenshotName = "photo" + hit.transform.name + ".png";

            // If folder contains a screenshot with this name - screenshot is already taken
            string[] files = System.IO.Directory.GetFiles(Application.dataPath + "/Resources/CameraScreenshots/");

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

            playerSource.Play();
            screenshotsTaken++;
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
            string filename = Application.dataPath + "/Resources/CameraScreenshots/" + screenshotName;
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));

            if (screenshotsTaken >= 4)
            {
                taskManager.taskEnd("Take pictures");
            }
        }
    }

    public void TurnOn()
    {
        isOn = !isOn;

        if (isOn)
        {
            tablet.materials[0].mainTexture = tabletRender;
            tablet.materials[0].color = Color.white;
            canvas.gameObject.SetActive(true);
        }
        else
        {
            tablet.materials[0].mainTexture = null;
            tablet.materials[0].color = Color.black;
            canvas.gameObject.SetActive(false);
        }
    }
}
