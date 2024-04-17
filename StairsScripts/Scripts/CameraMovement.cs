using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float minViewDistance = 12f;
    public float Sensitivity;
    public Transform player;
    float xRotation = 0f;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start() {
        Sensitivity = PlayerPrefs.GetFloat("Sens");
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, minViewDistance);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }


}
