using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public CameraController CameraController;
    public Animator Animator;

    public bool Autostart;

    private void Awake()
    {
        if (Autostart)
        {
            StartGame();
        }
    }

    private void Start()
    {
        if (!Autostart)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {

            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void StartGame()
    {
        CameraController.FollowPlayer();
        Animator.SetTrigger("FadeOut");
    }

}
