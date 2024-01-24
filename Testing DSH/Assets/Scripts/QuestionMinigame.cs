using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionMinigame : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI yesButtonText;
    public TextMeshProUGUI noButtonText;

    private string[] questions = new string[10]; // Assuming you have 10 questions
    private int selectedCount = 0;
    private int currentQuestionIndex = 0;

    void Start()
    {
        InitializeQuestions();
        DisplayCurrentQuestion();
    }

    void InitializeQuestions()
    {
        // Assuming you have predefined questions
        questions[0] = "Question 1";
        questions[1] = "Question 2";
        questions[2] = "Question 3";
        questions[3] = "Question 4";
        questions[4] = "Question 5";
        questions[5] = "Question 6";
        questions[6] = "Question 7";
        questions[7] = "Question 8";
        questions[8] = "Question 9";
        questions[9] = "Question 10";
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

    bool IsQuestionSelected(string question)
    {
        // Check if the question is already selected
        // Implement your logic here based on how you store selected questions
        return false;
    }
}

