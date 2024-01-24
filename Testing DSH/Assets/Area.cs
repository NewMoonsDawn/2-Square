using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<ScreenshotCamera>().area = this;
    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<ScreenshotCamera>().area = null;
    }
}
