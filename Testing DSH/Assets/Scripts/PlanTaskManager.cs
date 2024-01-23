using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanTaskManager : MonoBehaviour
{

    private List<Transform> locations = new List<Transform>();
    private List<Transform> selectedLocations = new List<Transform>();
    public int expectedLocations = 3;
    public GameObject player;
    public TaskManager taskManager;

    // Start is called before the first frame update
    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            locations.Add(transform.GetChild(i).transform);
            locations[i].gameObject.AddComponent<MeshCollider>().enabled = false;
            locations[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < expectedLocations; i++)
        {
            Debug.Log(i);
            int random = Random.Range(0, locations.Count);
            selectedLocations.Add(locations[random]);
            locations.RemoveAt(random);
            Color temp = selectedLocations[i].GetComponent<Renderer>().material.color;
            temp.a = 0.6f;
            selectedLocations[i].GetComponent<Renderer>().material.color = temp;
            selectedLocations[i].gameObject.SetActive(true);
            Debug.Log(selectedLocations[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < selectedLocations.Count; i++)
        {
            if (Vector3.Distance(player.transform.position, selectedLocations[i].position) < 1f && Input.GetButtonDown("XRI_Left_SecondaryButton"))
            {
                Color temp = selectedLocations[i].GetComponent<Renderer>().material.color;
                temp.a = 1f;
                selectedLocations[i].GetComponent<Renderer>().material.color = temp;
                selectedLocations[i].GetComponent<MeshCollider>().enabled = true;
                PlayerPrefs.SetFloat("score", PlayerPrefs.GetFloat("score") + 100f);
                selectedLocations.RemoveAt(i);
            }
        }
        if (selectedLocations.Count == 0)
        {
            PlayerPrefs.SetFloat("score", PlayerPrefs.GetFloat("score") + 500f);
            taskManager.taskEnd("Plant Plants");
            gameObject.SetActive(false);
        }
    }
}
