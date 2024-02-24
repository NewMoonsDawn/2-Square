using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public static Player player;
    
    public TMP_InputField nameInput;
    // Start is called before the first frame update
    private void Start()
    {
        nameInput.enabled = false;
        player = new Player("Empty", 0f);
    }
    public void playGame()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        //SceneManager.LoadScene()
        nameInput.enabled = true;
    }

    public void nameInputted()
    {
        PlayerPrefs.SetString("name",nameInput.text);
        PlayerPrefs.SetFloat("score", 0f);
        Debug.Log(player.getName());
        SceneManager.LoadScene(1);
    }
}