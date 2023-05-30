using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSController : MonoBehaviour
{
    //[SerializeField] private Transform shoulderCamera;
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private GameObject character;

    [SerializeField] private float LookDistance = 20.0f;
    [SerializeField] private float mouseSensitivity;


    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        Rotate();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Rotate()
    {
        //character.transform.position = Camera.main.transform.position + Camera.main.transform.forward * LookDistance;
        //Vector3 point = character.transform.position;
        Vector3 point = Camera.main.transform.position + Camera.main.transform.forward * LookDistance;
        point.y = character.transform.position.y;
        //point.y = 0;
        character.transform.LookAt(point);
    }

    private void Look()
    {
        yRotation += lookDelta.x * mouseSensitivity;
        xRotation -= lookDelta.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraRoot.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
