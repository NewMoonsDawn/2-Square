using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    // Start is called before the first frame update


    private int currentTasks = 0;
    [SerializeField]
    private int expectedTasks = 3;

    private List<Task> tasksList = new List<Task>();
    private List<Task> tasks = new List<Task>();

    public TMP_Text taskText;

    public TMP_Text scoreText;
   /* private string task1;
    private string task2;
    private string task3;
    private string task4;
*/
    private string[] taskTexts;

    private Task task;
    void Start()
    {

        Debug.Log(PlayerPrefs.GetString("name"));
        taskTexts = new string[expectedTasks];
        for(int i =0;i<expectedTasks;i++)
        {
            taskTexts[i] = "";
        }

        tasksList.Add(new Task(15f, "Example1", "Desc1", 100f));
        tasksList.Add(new Task(12f, "Example2", "Desc2", 100f));
        tasksList.Add(new Task(11f, "Example3", "Desc3", 100f));
        tasksList.Add(new Task(14f, "Example4", "Desc4", 100f));
        tasksList.Add(new Task(16f, "Example5", "Desc5", 100f));
        tasksList.Add(new Task(9f, "Example6", "Desc6", 100f));

        taskText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = "Score: " + PlayerPrefs.GetFloat("score").ToString();
        currentTasks = tasks.Count;
      // if(tasksList.Count<expectedTasks-1)
       // {
         //   expectedTasks = tasksList.Count;
       //}    

        
       if(currentTasks < expectedTasks && tasksList.Count !=0)
        {
            int random = UnityEngine.Random.Range(0,tasksList.Count);
            task = tasksList[random];
            tasks.Add(task);
            tasksList.RemoveAt(random);
                
            /*switch (currentTasks+1)
            {
                case 1:
                    {
                        taskTexts[0] = task.getName() + ": " + task.getDescription();
                        break;
                    }
                case 2:
                    {
                        taskTexts[1]= task.getName() + ": " + task.getDescription();
                        break;
                    }
                    case 3:
                    {
                        taskTexts[2] = task.getName() + ": " + task.getDescription();
                        break;
                    }
                    case 4:
                    {
                        taskTexts[3] = task.getName() + ": " + task.getDescription();
                        break;
                    }
            }*/
            currentTasks++;
        }

       for(int i = 0; i < tasks.Count; i++)
        {
            
            tasks[i].setTime(tasks[i].getTime() - Time.deltaTime);
            if (tasks[i].getTime()<=0f)
            {
                Debug.Log(tasks[i].getName());
                tasks.RemoveAt(i);
            }
        }
        
       switch(tasks.Count)
        { 
            case 0:
                {
                    taskText.text = "You've completed all your tasks for the day! Sit back and relax!";
                    break;
                }
            case 1:
                {
                    taskText.text = taskText.text = tasks[0].getName() + " : " + tasks[0].getDescription() + string.Format(" {0}:{1}", Mathf.FloorToInt(tasks[0].getTime() / 60).ToString(), Mathf.FloorToInt(tasks[0].getTime() % 60).ToString());
                    break;
                }
            case 2:
                {
                    taskText.text = tasks[0].getName() + " : " + tasks[0].getDescription() + string.Format(" {0}:{1}", Mathf.FloorToInt(tasks[0].getTime() / 60).ToString(), Mathf.FloorToInt(tasks[0].getTime() % 60).ToString())
            + System.Environment.NewLine + tasks[1].getName() + " : " + tasks[1].getDescription() + string.Format(" {0}:{1}", Mathf.FloorToInt(tasks[1].getTime() / 60).ToString(), Mathf.FloorToInt(tasks[1].getTime() % 60).ToString());
                    break;
                }
            case 3:
                {
                    taskText.text = tasks[0].getName() + " : " + tasks[0].getDescription() + string.Format(" {0}:{1}", Mathf.FloorToInt(tasks[0].getTime() / 60).ToString(), Mathf.FloorToInt(tasks[0].getTime() % 60).ToString())
            + System.Environment.NewLine + tasks[1].getName() + " : " + tasks[1].getDescription() + string.Format(" {0}:{1}", Mathf.FloorToInt(tasks[1].getTime() / 60).ToString(), Mathf.FloorToInt(tasks[1].getTime() % 60).ToString())
            + System.Environment.NewLine + tasks[2].getName() + " : " + tasks[2].getDescription() + string.Format(" {0}:{1}", Mathf.FloorToInt(tasks[2].getTime() / 60).ToString(), Mathf.FloorToInt(tasks[2].getTime() % 60).ToString());
                    break;
                }
        
        }
        
    }
}
