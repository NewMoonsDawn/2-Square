using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class VideoSnap : MonoBehaviour
{
    public Texture2D[] photosTaken = new Texture2D[4];
    public XRSocketInteractor[] interactors;

    [Header("Rendering")]
    public Slider progressSlider;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI processingText;

    private bool rendered;
    private bool taskCompleted;

    private float scorePoints = 0f;

    private void Awake()
    {
        FindScreenshotTextures();
    }

    void FindScreenshotTextures()
    {
        // Look through directory and add screenshots to an array of textures
        for (int i = 0; i < photosTaken.Length; i++)
            photosTaken[i] = Resources.Load<Texture2D>("CameraScreenshots/photo" + i);
    }

    public void CheckIfComplete()
    {
        // Check if all XR Interactors have an object in them
        for (int i = 0; i < interactors.Length; i++)
        {
            // If an interactor doesn't have an object - task isn't complete
            if (interactors[i].GetOldestInteractableSelected() == null)
            {
                taskCompleted = false;
                return;
            }
        }

        // Else task is completed
        print("Task completed");
        taskCompleted = true;
        StartCoroutine(RenderVideo());
    }

    public void CalculateScore()
    {
        if (!taskCompleted) return;

        // Calculate Score
        for (int i = 0; i < interactors.Length; i++)
        {
            // Check if indexes of building blocks match the index of the interactor - if they do increment the score
            if (interactors[i].GetComponent<EditingZone>().index == interactors[i].GetOldestInteractableSelected().transform.GetComponent<EditingBlock>().index)
            {
                UpdateScore(100f);
            }
        }

        // Disable tutorial
        EditTutorial.instance.gameObject.SetActive(false);

        // Start rendering the video
        StartCoroutine(RenderVideo());
    }

    public IEnumerator RenderVideo()
    {
        yield return new WaitForSeconds(1);

        if (rendered) yield break;

        rendered = true;

        YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();

        // Setup progress slider and text
        float progress = 0;
        progressSlider.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);

        // Disable interactors so no more interaction can be done
        for (int i = 0; i < interactors.Length; i++)
            interactors[i].socketActive = false;

        // Fill up render progress slider over time
        while (progress < 1)
        {
            progress += .2f * Time.fixedDeltaTime;
            progressSlider.value = progress;
            yield return waitForFixedUpdate;
        }

        string keyword = "";

        // Calculate score and give a keyword based on how good the player edited the video
        switch (GameManager.instance.score)
        {
            case 0: keyword = "Poor..."; scorePoints = 200f; break;
            case 1: keyword = "Decent."; scorePoints = 350f; break;
            case 2: keyword = "Good!"; scorePoints = 500f; break;
            case 3: keyword = "Amazing!!!"; scorePoints = 1000f; break;
        }

        processingText.text = "Done!";
        scoreText.text = "Video Quality is " + keyword;
        UpdateScore(scorePoints);
    }

    public void UpdateScore(float score)
    {
        PlayerPrefs.SetFloat("score", PlayerPrefs.GetFloat("score") + score);
        Debug.Log(PlayerPrefs.GetFloat("score"));
    }
}
