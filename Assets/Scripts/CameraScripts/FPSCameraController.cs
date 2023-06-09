using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class FPSCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private Transform characterHead;
    [SerializeField] private float mouseSensitivity;
    

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 를 가운데에 묶어두기
        //CursorLockMode.Confined 는 윈도우 창 안넘어가게
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        RotateCharacter();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        yRotation += lookDelta.x * mouseSensitivity; // 마우스 좌우 기준 x 는 y축을 기준으로 회전
        xRotation -= lookDelta.y * mouseSensitivity; // 마우스 상하 기준 y 는 x 축을 기준으로 회전
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // 상하 80 도 제한
        //yRotation = Mathf.Clamp(yRotation, -80f, 80f);
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        //characterHead.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void RotateCharacter() 
    {
        Vector3 point = Camera.main.transform.position + Camera.main.transform.forward;
      
        point.y = characterHead.transform.position.y;
        characterHead.transform.LookAt(point);
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
