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
        for (int i = 0; i < photosTaken.Length; i++)
        {
            photosTaken[i] = Resources.Load<Texture2D>("CameraScreenshots/photo" + i);
        }
    }

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
                UpdateScore(100f);
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
