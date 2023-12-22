using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class VideoSnap : MonoBehaviour
{
    public XRSocketInteractor[] interactors;

    [Header("Rendering")]
    public Slider progressSlider;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI processingText;

    private bool rendered;
    private bool taskCompleted;

    public void CheckIfComplete()
    {
        for (int i = 0; i < interactors.Length; i++)
        {
            if (interactors[i].GetOldestInteractableSelected() == null)
            {
                taskCompleted = false;
                return;
            }
        }

        print("Task completed");
        taskCompleted = true;
        EditTutorial.instance.NextPhrase();
    }

    public void CalculateScore()
    {
        if (!taskCompleted) return;

        // Calculate Score
        for (int i = 0; i < interactors.Length; i++)
        {
            if (interactors[i].GetComponent<EditingZone>().index == interactors[i].GetOldestInteractableSelected().transform.GetComponent<EditingBlock>().index)
            {
                GameManager.instance.score++;
            }
        }

        EditTutorial.instance.gameObject.SetActive(false);

        StartCoroutine(RenderVideo());
    }

    public IEnumerator RenderVideo()
    {
        if (rendered) yield break;

        rendered = true;

        YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();

        float progress = 0;
        progressSlider.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);

        for (int i = 0; i < interactors.Length; i++)
            interactors[i].socketActive = false;

        while (progress < 1)
        {
            progress += .2f * Time.fixedDeltaTime;
            progressSlider.value = progress;
            yield return waitForFixedUpdate;
        }

        string keyword = "";

        // Calculate score
        switch (GameManager.instance.score)
        {
            case 0: keyword = "Poor..."; break;
            case 1: keyword = "Decent."; break;
            case 2: keyword = "Good!"; break;
            case 3: keyword = "Amazing!!!"; break;
        }

        processingText.text = "Done!";
        scoreText.text = "Video Quality is " + keyword;
    }
}
