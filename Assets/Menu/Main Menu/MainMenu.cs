using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public CameraController CameraController;
    public Animator Animator;
    public CanvasGroup CanvasGroup;

    public bool Autostart;

    [NonSerialized] public bool Started = false;

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

        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
        Cursor.lockState = CursorLockMode.Locked;
        Started = true;
    }

}
