using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MeetingBlock : MonoBehaviour
{
    // Start is called before the first frame update

    public MeetingBehaviour meetingBehaviour;

    void Start()
    {
        meetingBehaviour = transform.parent.GetComponent<MeetingBehaviour>();
        transform.AddComponent<BoxCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.name == "Player")
        {
            meetingBehaviour.playerEntered();
        }
    }
}
