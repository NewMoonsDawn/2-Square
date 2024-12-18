using UnityEngine;
using UnityEngine.UI;

public class ObjectMovement : MonoBehaviour
{
    public GameObject laptopTranslucent;
    public GameObject laptopSolid;
    public GameObject laptop;
    public GameObject highlight;
    public TaskManager taskManager;

    private Rigidbody laptopRigidbody;
    private bool isMoving = false;

    private void Start()
    {
        laptopRigidbody = laptop.GetComponent<Rigidbody>();
        highlight.SetActive(true);
        laptopTranslucent.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerObject"))
        {
            Debug.Log("Works");

            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("LaptopTranslucent");

            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            laptopSolid.SetActive(true);
            laptop.SetActive(false);
            highlight.SetActive(false);

            taskManager.taskEnd("Lost Laptop");
        }
    }
}
