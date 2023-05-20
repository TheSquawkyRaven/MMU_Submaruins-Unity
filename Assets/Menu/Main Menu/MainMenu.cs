using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private static MainMenu instance;
    public static MainMenu Instance => instance;
    

    public CameraController CameraController;
    public Animator Animator;
    public CanvasGroup CanvasGroup;

    public bool Autostart;

    [NonSerialized] public bool Started = false;

    public GameObject[] StartedObjects;

    private void Awake()
    {
        instance = this;
        if (Autostart)
        {
            StartGame();
        }
        else
        {
            for (int i = 0; i < StartedObjects.Length; i++)
            {
                StartedObjects[i].SetActive(false);
            }
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
        GiveItemsOnStart();

        for (int i = 0; i < StartedObjects.Length; i++)
        {
            StartedObjects[i].SetActive(true);
        }
    }

    private void GiveItemsOnStart()
    {
        // Gives a battery that lasts 5 minutes
        PlayerInventory.Instance.AddItem(1, new()
        {
            amount = 1,
            float1 = new()
            {
                300
            }
        });
    }

}
