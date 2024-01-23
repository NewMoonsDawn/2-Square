using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FreeView : MonoBehaviour
{
    public float sensitivity = 1;
    public float speed = 15;
    public float smooth = .125f;

    private Vector3 movement;
    private Vector3 euler;
    private float mouseX, mouseY;


    void Update()
    {
        movement = (transform.right * Input.GetAxisRaw("Horizontal") + Vector3.up * Input.GetAxisRaw("CameraView") + transform.forward * Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;
        mouseX += Input.GetAxis("Mouse Y") * sensitivity * -1;
        mouseY += Input.GetAxis("Mouse X") * sensitivity;
        euler = new Vector3(mouseX, mouseY, 0);

        transform.position += movement;
        transform.eulerAngles = euler;
    }
}
