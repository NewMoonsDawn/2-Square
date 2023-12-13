using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VideoSnap : MonoBehaviour
{
    public XRSocketInteractor[] interactors;
    public GameObject renderButton;

    public void CheckIfComplete()
    {
        for (int i = 0; i < interactors.Length; i++)
        {
            if (interactors[i].GetOldestInteractableSelected() == null)
            {
                // Deactivate Button
                renderButton.SetActive(false);
                return;
            }
        }

        print("Task completed");

        // Activate Button
        renderButton.SetActive(true);
    }

    public void CalculateScore()
    {
        // Calculate Score
        for (int i = 0; i < interactors.Length; i++)
        {
            if (interactors[i].GetComponent<EditingZone>().index == interactors[i].GetOldestInteractableSelected().transform.GetComponent<EditingBlock>().index)
            {
                GameManager.instance.score++;
            }
        }
    }
}
