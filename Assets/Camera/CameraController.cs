using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;
    public static CameraController Instance => instance;

    public CinemachineVirtualCamera MainMenuVCam;
    public CinemachineVirtualCamera PlayerVCam;

    public AudioSource ClickAudio;

    private void Awake()
    {
        instance = this;
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

    public void PlayClickSound()
    {
        ClickAudio.Play();
    }

}
