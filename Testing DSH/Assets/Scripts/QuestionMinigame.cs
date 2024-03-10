using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionMinigame : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button yesButton;
    public Button noButton;

    private string[] questions = new string[11]; // Assuming you have 10 questions
    private int selectedCount = 0;
    private int currentQuestionIndex = 0;
    [SerializeField]
    public int expectedSelected = 5;

    public TaskManager taskManager;
    public List<string> answers = new List<string>();
    private string finalPopup = "Your selected questions are: " + System.Environment.NewLine;
    private bool finished = false;
    void Start()
    {
        InitializeQuestions();
        DisplayCurrentQuestion();
    }

    void InitializeQuestions()
    {
        // Assuming you have predefined questions

        questions[0] = "Hello Intern! You have to pick 5 out the 10 questions shown to you for an interview we have tomorrow with a possible intern. Best of luck! ";
        questions[1] = "Who are you?";
        questions[2] = "What would you say your main skills are?";
        questions[3] = "Why are you interested in working for us?";
        questions[4] = "What was your name again?";
        questions[5] = "Tell me about yourself.";
        questions[6] = "What do you know about the DSH?";
        questions[7] = "Is there a DSH project that caught your interest?";
        questions[8] = "Why did you bother coming?";
        questions[9] = "Would you be willing to deliver me coffee as part of your internship?";
        questions[10] = "What can this position help you learn?";
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
            answers.Add(questions[currentQuestionIndex]);
        }
        if(selectedCount>=expectedSelected)
        {
            taskManager.taskEnd("Interview Setup");
            foreach(var answer in answers)
            {
                finalPopup += answer.ToString() + System.Environment.NewLine;
            }
            questionText.text = finalPopup;
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            finished = true;
        }
        currentQuestionIndex++;
        if(currentQuestionIndex==11 && selectedCount<5)
        {
            currentQuestionIndex = 0;
            answers.Clear();
        }
        if(!finished)
        DisplayCurrentQuestion();
    }

    public void NoButton()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex == 11 && selectedCount < 5)
        {
            currentQuestionIndex = 0;
            answers.Clear();
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

