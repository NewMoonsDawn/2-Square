using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Cup cup))
        {
            if (cup.fill < 95) return;
            cup.fill = 0;
            cup.interactable = false;
            cup.fillTransform.localPosition = Vector3.zero;
            print("Here");
            // Add Score
        }
    }
}
