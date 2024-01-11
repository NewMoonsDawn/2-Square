using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    private ParticleSystem pour;

    void Start()
    {
        pour = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Cup>())
        {
            if (other.GetComponent<Cup>().fill < 100) pour.Play();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Cup>())
        {
            other.GetComponent<Cup>().Fill(1);
            if (other.GetComponent<Cup>().fill >= 100) pour.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pour.Stop();
    }

}
