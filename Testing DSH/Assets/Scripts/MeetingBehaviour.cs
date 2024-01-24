using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class MeetingBehaviour : MonoBehaviour
{
    public GameObject player;


    public float spawntimer;
    public int children;
    public Transform[] spawnLocations;
    public float actiontimer;
    public Transform currentLocation;

    [SerializeField]
    public float spawntime = 80f;
    [SerializeField]
    public float actiontime = 80f;

    public bool action = false;

    public TMP_Text actionUI;

    private Color transparent = new Color(255,255,255,0f);

    //TODO FINAL MESSAGE
    public string meetingQuestMessage = "There is an important meeting going on! Find and attend it, quick!";

    // Start is called before the first frame update
    void Start()
    {
        children = transform.childCount;
        spawnLocations = new Transform[children];
        spawntimer = spawntime;
        actionUI.transform.parent.gameObject.SetActive(false);
        for (int i = 0; i<children; i++)
        {
            spawnLocations[i] = transform.GetChild(i);
            spawnLocations[i].GetComponent<Renderer>().enabled = false;
            Debug.Log(spawnLocations[i].name);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (spawntimer > 0f)
        {
            spawntimer -= Time.deltaTime;
        }
        else if (!action)
        {
            action = true;
            currentLocation = spawnLocations[UnityEngine.Random.Range(0, children)];
            currentLocation.GetComponent<Renderer>().enabled = true;
            currentLocation.GetComponent<Renderer>().material.color = Color.green;
            actionUI.transform.parent.gameObject.SetActive(true);
            actiontimer = actiontime;
        }

        if(actiontimer> 0f) 
        {
            actiontimer -= Time.deltaTime;
            actionUI.text = meetingQuestMessage + string.Format(" {0}:{1}", Mathf.FloorToInt(actiontimer / 60).ToString(), Mathf.FloorToInt(actiontimer % 60).ToString());
            if(Vector3.Distance(player.transform.position,currentLocation.position)<2.5f)
            {
                actiontimer = 0f;
                PlayerPrefs.SetFloat("score", PlayerPrefs.GetFloat("score") + 200f);
            }
        }
        else if (action)
        {
            spawntimer = spawntime;
            //currentLocation.GetComponent<Renderer>().material.color = transparent;
            currentLocation.GetComponent<Renderer>().enabled = false;
            actionUI.transform.parent.gameObject.SetActive(false);
            action = false;
        }
    }
}
