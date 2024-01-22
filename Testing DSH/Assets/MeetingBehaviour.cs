using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class MeetingBehaviour : MonoBehaviour
{
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
            Debug.Log(spawnLocations[i]);
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
            currentLocation.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
            actionUI.transform.parent.gameObject.SetActive(true);
            actiontimer = actiontime;
        }

        if(actiontimer> 0f) 
        {
            actiontimer -= Time.deltaTime;
            actionUI.text = meetingQuestMessage + string.Format(" {0}:{1}", Mathf.FloorToInt(actiontimer / 60).ToString(), Mathf.FloorToInt(actiontimer % 60).ToString());
        }
        else if (action)
        {
            spawntimer = spawntime;
            currentLocation.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
            actionUI.transform.parent.gameObject.SetActive(false);
            action = false;
        }
    }
}
