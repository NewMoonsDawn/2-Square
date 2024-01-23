using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public Transform headTransform;
    public bool interactable;
    public float fill;
    public ParticleSystem pour;
    public Transform fillTransform;
    public bool filled;

    private void FixedUpdate()
    {
        if (!interactable) return;

        fillTransform.localPosition = Vector3.up * ((fill + .1f) / 100) * 2;
    }

    public void Fill(float amount)
    {
        if (fill < -.1f) { fill = 0; return; }
        if (fill > 100.1f)
        { 
            fill = 100; 
            if (!filled)
            {
                UpdateScore(250f);
                filled = true;
            }

            return; 
        }

        fill += amount;
    }

    public void UpdateScore(float score)
    {
        PlayerPrefs.SetFloat("score", PlayerPrefs.GetFloat("score") + score);
        Debug.Log(PlayerPrefs.GetFloat("score"));
    }
}
