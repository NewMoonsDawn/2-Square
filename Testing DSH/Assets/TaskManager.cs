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

    public TMP_Text uiText;

    private string task1;
    private string task2;
    private string task3;
    private string task4;

    private Task task;
    void Start()
    {
        tasksList.Add(new Task(90f, "Example1", "Desc1", 100f));
        tasksList.Add(new Task(90f, "Example2", "Desc2", 100f));
        tasksList.Add(new Task(90f, "Example3", "Desc3", 100f));
        tasksList.Add(new Task(90f, "Example4", "Desc4", 100f));
        tasksList.Add(new Task(90f, "Example5", "Desc5", 100f));

        uiText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
       if(currentTasks < expectedTasks)
        {
            int random = UnityEngine.Random.Range(0,tasksList.Count-1);
            Debug.Log(random.ToString());
            task = tasksList[random];
            tasks.Add(task);
            tasksList.RemoveAt(random);
                
            switch (currentTasks+1)
            {
                case 1:
                    {
                        task1 = task.getName() + ": " + task.getDescription();
                        break;
                    }
                case 2:
                    {
                        task2= task.getName() + ": " + task.getDescription();
                        break;
                    }
                    case 3:
                    {
                        task3 = task.getName() + ": " + task.getDescription();
                        break;
                    }
                    case 4:
                    {
                        task4 = task.getName() + ": " + task.getDescription();
                        break;
                    }
            }
            currentTasks++;
        }
        uiText.text = task1 + System.Environment.NewLine + task2 + System.Environment.NewLine + task3 + System.Environment.NewLine + task4;
    }
}
