using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanTaskManager : MonoBehaviour
{

    private List<Transform> locations;
    private List<Transform> selectedLocations;
    public int expectedLocations = 2;
    public GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            locations.Add(transform.GetChild(i).transform);
            locations[i].gameObject.AddComponent<MeshCollider>().enabled = false;
            locations[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < expectedLocations; i++)
        {
            int random = Random.Range(0, locations.Count);
            selectedLocations.Add(locations[random]);
            locations.RemoveAt(random);
            Color temp = selectedLocations[i].GetComponent<Renderer>().material.color;
            temp.a = 0.6f;
            locations[i].GetComponent<Renderer>().material.color = temp;
            selectedLocations[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < selectedLocations.Count; i++)
        {
            /*if (Vector3.Distance(player.transform.position, selectedLocations[i].position) < 1f && Input.GetButtonDown("XRI_Left_SecondaryButton"))
            {
                Color temp = selectedLocations[i].GetComponent<Renderer>().material.color;
                temp.a = 1f;
                selectedLocations[i].GetComponent<Renderer>().material.color = temp;
                selectedLocations[i].GetComponent<MeshCollider>().enabled = true;
                PlayerPrefs.SetFloat("score", PlayerPrefs.GetFloat("score") + 100f);
                selectedLocations.RemoveAt(i);
            }*/
        }
        if (selectedLocations.Count == 0)
        {
            PlayerPrefs.SetFloat("score", PlayerPrefs.GetFloat("score") + 500f);
            gameObject.SetActive(false);
        }
    }
}
