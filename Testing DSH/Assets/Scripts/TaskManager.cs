using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TaskManager : MonoBehaviour
{
    // Start is called before the first frame update


    private int currentTasks = 0;
    [SerializeField]
    private int expectedTasks = 2;

    private List<Task> tasksList = new List<Task>();
    private List<Task> tasks = new List<Task>();

    public TMP_Text taskText;

    public TMP_Text scoreText;

    public TMP_Text timeText;

    [SerializeField]
    public float totalGameTime;

    [Header("Minigame Attachments")]
    public Cup coffeeScript;
    public ScreenshotCamera screenshotCameraScript;
    public PlanTaskManager plantTaskScript;
    public GameObject videoEditting;
    public Canvas interviewMinigame;
    public GameObject lostLaptop;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip taskNotification;
    public AudioClip taskComplete;

    [Header("End Screen Attachments")]
    private bool startingBatch = true;
    private bool finished = false;


    public Canvas endCanvas;
    public ActionBasedContinuousMoveProvider movement;
    public TMP_Text endText;
    // public TMP_Text debug;



    /* private string task1;
     private string task2;
     private string task3;
     private string task4;
 */
    private string[] taskTexts;

    private Task task;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerPrefs.SetFloat("score", 0f);
        Debug.Log(PlayerPrefs.GetString("name"));
        taskTexts = new string[expectedTasks];
        for (int i = 0; i < expectedTasks; i++)
        {
            taskTexts[i] = "";
        }

        tasksList.Add(new Task(120f, "Interview Setup", "Go to your computer at the end of the lounge area and set up your interview questions!", 350f));
        tasksList.Add(new Task(110f, "Plant Plants", "People said they wanted more plants around the buildings. Find appropriate places and plant some plants!", 120f));
        tasksList.Add(new Task(130f, "Get a Coffee", "Time for a break, go get some coffee from the cafeteria!", 100f));
        tasksList.Add(new Task(115f, "Take Video", "We've left you a tablet on the large orange table in the lounge, record a 10 second video with it!", 200f));
        tasksList.Add(new Task(105f, "Video Editting", "Room 0.39, attach the video blocks correctly by grabbing them and putting them into place.", 180f));
        tasksList.Add(new Task(90f, "Lost Laptop", "Oops! You've lost your white laptop somewhere in the lounge. Find it and bring it back to 0.40!", 150f));
        taskText.text = "";
        videoEditting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            scoreText.text = "Score: " + PlayerPrefs.GetFloat("score").ToString();
            currentTasks = tasks.Count;
            // if(tasksList.Count<expectedTasks-1)
            // {
            //   expectedTasks = tasksList.Count;
            //}    
            if (currentTasks < expectedTasks && tasksList.Count != 0)
            {
                int random = UnityEngine.Random.Range(0, tasksList.Count);
                task = tasksList[random];
                tasks.Add(task);
                if (!startingBatch)
                {
                    audioSource.PlayOneShot(taskNotification, 1f);

                }
                else startingBatch = false;
                tasksList.RemoveAt(random);

                switch (task.getName())
                {
                    case "Plant Plants":
                        {
                            plantTaskScript.gameObject.SetActive(true);
                            break;
                        }
                    case "Get a Coffee":
                        {
                            coffeeScript.interactable = true;
                            break;
                        }
                    case "Take Video":
                        {
                            screenshotCameraScript.interactable = true;
                            break;
                        }
                    case "Video Editting":
                        {
                            videoEditting.SetActive(true);
                            break;
                        }
                    case "Interview Setup":
                        {
                            interviewMinigame.gameObject.SetActive(true);
                            break;
                        }
                    case "Lost Laptop":
                        {
                            lostLaptop.SetActive(true);
                            break;
                        }

                }
                currentTasks++;
            }

            for (int i = 0; i < tasks.Count; i++)
            {

                tasks[i].setTime(tasks[i].getTime() - Time.deltaTime);
                if (tasks[i].getTime() <= 0f)
                {
                    Debug.Log(tasks[i].getName());
                    tasks.RemoveAt(i);
                }
            }

            switch (tasks.Count)
            {
                case 0:
                    {
                        taskText.text = "You've completed all your tasks for the day! Sit back and relax!";
                        break;
                    }
                case 1:
                    {
                        taskText.text = taskText.text = tasks[0].getName() + " : " + tasks[0].getDescription() + string.Format(" {0}s", Mathf.FloorToInt(tasks[0].getTime()).ToString());
                        break;
                    }
                case 2:
                    {
                        taskText.text = tasks[0].getName() + " : " + tasks[0].getDescription() + string.Format(" {0}s", Mathf.FloorToInt(tasks[0].getTime()).ToString())
                + System.Environment.NewLine + tasks[1].getName() + " : " + tasks[1].getDescription() + string.Format(" {0}s", Mathf.FloorToInt(tasks[1].getTime()).ToString());
                        break;
                    }
                    /*case 3:
                        {
                            taskText.text = tasks[0].getName() + " : " + tasks[0].getDescription() + string.Format(" {0}:{1}", Mathf.FloorToInt(tasks[0].getTime() / 60).ToString(), Mathf.FloorToInt(tasks[0].getTime() % 60).ToString())
                    + System.Environment.NewLine + tasks[1].getName() + " : " + tasks[1].getDescription() + string.Format(" {0}:{1}", Mathf.FloorToInt(tasks[1].getTime() / 60).ToString(), Mathf.FloorToInt(tasks[1].getTime() % 60).ToString())
                    + System.Environment.NewLine + tasks[2].getName() + " : " + tasks[2].getDescription() + string.Format(" {0}:{1}", Mathf.FloorToInt(tasks[2].getTime() / 60).ToString(), Mathf.FloorToInt(tasks[2].getTime() % 60).ToString());
                            break;
                        */
            }
            if (totalGameTime > 0f)
            {
                totalGameTime -= Time.deltaTime;
            }
            if (totalGameTime <= 0f)
            {
                EndGame();
            }
           timeText.text = string.Format("Time Remaining: {0}s", Mathf.FloorToInt(totalGameTime).ToString());
        }

    }

    public void taskEnd(string taskName)
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].getName() == taskName)
            {
                //debug.text = "task complete" + taskName;

                PlayerPrefs.SetFloat("score", PlayerPrefs.GetFloat("score") + tasks[i].getPoints());
                audioSource.PlayOneShot(taskComplete, 1f);
                tasks.RemoveAt(i);
            }
        }
    }
     
    public void EndGame()
    {
        movement.moveSpeed = 0;
        endCanvas.gameObject.SetActive(true);
        finished = true;
        endText.text = String.Format("Time is up! Your final score is: {0} points", PlayerPrefs.GetFloat("score").ToString());
    }
    }
