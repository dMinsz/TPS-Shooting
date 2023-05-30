using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraView : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera FPSCam;

    private bool isChanged = false;

    private void OnChangeLook()
    {
        if (!isChanged)
        {
            isChanged = true;
            FPSCam.Priority = 11;
        }
        else 
        {
            isChanged = false;
            FPSCam.Priority = 5;
        }
        
    }
}
