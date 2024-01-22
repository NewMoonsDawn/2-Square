using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Text questionText;
    public Button[] questionButtons;

    private List<string> availableQuestions = new List<string>();
    private List<string> selectedQuestions = new List<string>();

    void Start()
    {
        // Assuming you have predefined questions
        availableQuestions.Add("Question 1");
        availableQuestions.Add("Question 2");
        // Add other questions...

        // Display initial set of questions
        UpdateQuestionDisplay();
    }

    void UpdateQuestionDisplay()
    {
        // Display questions on the computer screen
        questionText.text = "Select 5 questions:\n";

        for (int i = 0; i < availableQuestions.Count; i++)
        {
            questionText.text += $"{i + 1}. {availableQuestions[i]}\n";
        }
    }

    public void SelectQuestion(int questionIndex)
    {
        // Add or remove questions based on player selection
        string selectedQuestion = availableQuestions[questionIndex];

        if (selectedQuestions.Contains(selectedQuestion))
        {
            selectedQuestions.Remove(selectedQuestion);
        }
        else
        {
            selectedQuestions.Add(selectedQuestion);
        }

        // Update the display after each selection
        UpdateQuestionDisplay();
    }

    public void StartInterview()
    {
        // Check if the player has selected exactly 5 questions
        if (selectedQuestions.Count == 5)
        {
            // Start the interview with the selected questions
            StartInterviewWithQuestions();
        }
        else
        {
            // Inform the player to select 5 questions
            Debug.Log("Please select exactly 5 questions.");
        }
    }

    void StartInterviewWithQuestions()
    {
        // Logic to start the interview with selected questions
        Debug.Log("Interview started with selected questions:");
        foreach (string question in selectedQuestions)
        {
            Debug.Log(question);
        }

        // Add your logic to transition to the interview scene or trigger other actions
    }
}