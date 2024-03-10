using UnityEngine;
using UnityEngine.UI;

public class ObjectMovement : MonoBehaviour
{
    public GameObject laptopTranslucent;
    public GameObject laptopSolid;
    public GameObject laptop;
    public GameObject highlight;

    private Rigidbody laptopRigidbody;
    private bool isMoving = false;

    private void Start()
    {
        laptopRigidbody = laptop.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (laptopRigidbody.velocity.magnitude > 2f)
        {
            isMoving = true;
            highlight.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerObject") && isMoving)
        {
            Debug.Log("Works");

            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("LaptopTranslucent");

            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            laptopTranslucent.SetActive(false);
            laptopSolid.SetActive(true);
            laptop.SetActive(false);
            highlight.SetActive(false);
            //task complete logic
        }
    }
}
