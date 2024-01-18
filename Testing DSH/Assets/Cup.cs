using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public float fill;
    public ParticleSystem pour;
    public Transform fillTransform;
    public bool filled;

    private void FixedUpdate()
    {
        fillTransform.localPosition = Vector3.up * ((fill + .1f) / 100) * 2;

        if (fill <= 0) return;

         if (transform.eulerAngles.z > 225 && transform.eulerAngles.z < 315)
        {
            Fill(-1);
            pour.Play();
        }
         else
        {
            pour.Stop();
        }
    }

    public void Fill(float amount)
    {
        if (fill < -.1f) { fill = 0; return; }
        if (fill > 100.1f)
        { 
            fill = 100; 
            if (!filled)
            {
                print("Got points for filling cup"); // Replace with actual score
                filled = true;
            }

            return; 
        }

        fill += amount;
    }
}
