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
            writingText = false;
            StopCoroutine("ChatboxTextType");
            chatText.text = dialogue[phraseCount - 1];
            ChooseNextPhrase();
        }
    }

    public void StartDialogue()
    {
        writingText = false;
        phraseCount = 0;
        ChooseNextPhrase();
    }

    public void ChooseNextPhrase()
    {
        if (!writingText)
        {
            if (phraseCount != dialogue.Count) // If last thing to say disable chatbox
                StartCoroutine(ChatboxTextType(dialogue[phraseCount]));
            phraseCount++;
        }
        else
        {
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
