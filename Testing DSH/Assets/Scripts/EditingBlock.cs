using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditingBlock : MonoBehaviour
{
    public int index;

    public void SetMaterial()
    {
        GetComponentInChildren<MeshRenderer>().materials[1].SetTexture("_MainTex", FindObjectOfType<VideoSnap>().photosTaken[index]);
    }

    private void Start()
    {
        SetMaterial();
    }
}
