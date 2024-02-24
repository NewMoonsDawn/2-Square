using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditingBlock : MonoBehaviour
{
    public int index;

    public void SetMaterial()
    {
        // Get the material of the screen and set the texture to a screenshot depending on the index
        GetComponentInChildren<MeshRenderer>().materials[1].SetTexture("_MainTex", FindObjectOfType<VideoSnap>().photosTaken[index]);
    }

    private void Start()
    {
        SetMaterial();
    }
}
