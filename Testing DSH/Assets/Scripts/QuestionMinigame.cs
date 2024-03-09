using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionMinigame : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI yesButtonText;
    public TextMeshProUGUI noButtonText;

    private string[] questions = new string[11]; // Assuming you have 10 questions
    private int selectedCount = 0;
    private int currentQuestionIndex = 0;
    [SerializeField]
    public int expectedSelected = 5;

    void Start()
    {
        InitializeQuestions();
        DisplayCurrentQuestion();
    }

    void InitializeQuestions()
    {
        // Assuming you have predefined questions

        questions[0] = "Hello Intern! You have to pick 5 out the 10 questions shown to you. Best of luck! " +
            "Its for an interview we have tomorrow with Corian.";
        questions[1] = "What can the DSH help you with?";
        questions[2] = "What are you looking for the DSH to help with?";
        questions[3] = "What type of Business are you?";
        questions[4] = "Do you know what the DSH does?";
        questions[5] = "How long will the partnership take?";
        questions[6] = "How do interns help grow the DSH?";
        questions[7] = "As an intern do you get a lot of tasks?";
        questions[8] = "Was your first day as an intern successful?";
        questions[9] = " Doing an internship at the DSH helps with?";
        questions[10] = "What is your connection with DSH?";
    }

    void DisplayCurrentQuestion()
    {
        questionText.text = questions[currentQuestionIndex];
    }

    public void SelectOption(bool isYes)
    {
        // Handle player's choice
        if (isYes)
        {
            // Logic for 'Yes' option
            // Add the selected question to your list if not already selected
            if (!IsQuestionSelected(questions[currentQuestionIndex]))
            {
                selectedCount++;
            }
        }

        // Move to the next question
        currentQuestionIndex = (currentQuestionIndex + 1) % questions.Length;

        // If all questions are displayed and not enough selected, reset
        if (currentQuestionIndex == 0 && selectedCount < 5)
        {
            Debug.Log("Not enough questions selected. Resetting...");
            selectedCount = 0;
        }

        // Display the next question
        DisplayCurrentQuestion();
        
    }
    public void YesButton ()
    {
        if (currentQuestionIndex != 0)
        {
            selectedCount++;
        }
        if(selectedCount>=expectedSelected)
        {
            //TODO end minigame
            Debug.Log("game end");
        }
        currentQuestionIndex++;
        if(currentQuestionIndex==10 && selectedCount<5)
        {
            currentQuestionIndex = 0;
        }
        DisplayCurrentQuestion();
    }

    public void NoButton()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex == 10 && selectedCount < 5)
        {
            currentQuestionIndex = 0;
        }
        DisplayCurrentQuestion();
    }

    bool IsQuestionSelected(string question)
    {
        // Check if the question is already selected
        // Implement your logic here based on how you store selected questions
        return false;
    }
}

