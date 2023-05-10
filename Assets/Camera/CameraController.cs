using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    public CinemachineVirtualCamera MainMenuVCam;
    public CinemachineVirtualCamera PlayerVCam;

    private void Awake()
    {
        MainMenuVCam.Priority = 100;
        PlayerVCam.Priority = 0;
    }

    public void FollowPlayer()
    {
        MainMenuVCam.Priority = 0;
        PlayerVCam.Priority = 100;
    }

    public void GoToMainMenu()
    {
        MainMenuVCam.Priority = 100;
        PlayerVCam.Priority = 0;
    }

}
