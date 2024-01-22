using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditTutorial : MonoBehaviour
{
    public static EditTutorial instance;

    public bool writingText;
    public TextMeshProUGUI chatText;
    public int phraseCount;
    public List<string> dialogue;

    private void Start()
    {
        instance = this;
        StartDialogue();
    }

    public void NextPhrase()
    {
        if (!writingText) ChooseNextPhrase();
        else
        {
            // Skip typewriter effect
            writingText = false;
            StopCoroutine("ChatboxTextType");
            chatText.text = dialogue[phraseCount - 1];
            ChooseNextPhrase();
        }
    }

    public void StartDialogue()
    {
        // Refresh dialogue
        writingText = false;
        phraseCount = 0;
        ChooseNextPhrase();
    }

    public void ChooseNextPhrase()
    {
        if (!writingText)
        {
            if (phraseCount != dialogue.Count) // If there is still something to say, keep invoking the typewriter effect
                StartCoroutine(ChatboxTextType(dialogue[phraseCount]));
            phraseCount++;
        }
        else
        {
            // If last thing to say stop writing
            StopCoroutine("ChatboxTextType");
            chatText.text = dialogue[phraseCount - 1];
            writingText = false;
        }
    }

    public IEnumerator ChatboxTextType(string phrase)
    {
        chatText.text = "";
        char[] phraseChars = phrase.ToCharArray();
        writingText = true;

        // Typewriter effect
        for (int i = 0; i < phraseChars.Length; i++)
        {
            if (!writingText)
            {
                chatText.text = phrase;
                break;
            }
            chatText.text += phraseChars[i];
            yield return new WaitForSeconds(.045f);
        }

        writingText = false;
    }
}
