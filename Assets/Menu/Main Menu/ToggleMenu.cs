using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    private static ToggleMenu instance;
    public static ToggleMenu Instance => instance;

    public GameObject Menu;
    

    public CameraController CameraController;
    public CanvasGroup CanvasGroup;

    [NonSerialized] public bool Started = false;

    public GameObject[] StartedObjects;

    private float alphaFadeTime;
    public float AlphaFade;

    private void Awake()
    {
        Time.timeScale = 1;
        Menu.SetActive(false);
        Menu.GetComponent<Menu>().Title.SetActive(false);
        instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        for (int i = 0; i < StartedObjects.Length; i++)
        {
            StartedObjects[i].SetActive(false);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerInventory.Instance.IsActive && PlayerInventory.Instance.gameObject.activeSelf)
            {
                PlayerInventory.Instance.EnableInventory(false);
                return;
            }
            if (Menu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        StartCoroutine(LerpCanvasAlpha(true));
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        StartCoroutine(LerpCanvasAlpha(false));
        Cursor.lockState = CursorLockMode.Locked;
    }


    public IEnumerator LerpCanvasAlpha(bool activating)
    {
        Menu.SetActive(true);
        alphaFadeTime = AlphaFade;
        while (true)
        {
            alphaFadeTime -= Time.unscaledDeltaTime;
            if (alphaFadeTime <= 0)
            {
                if (activating)
                {
                    CanvasGroup.alpha = 1;
                }
                else
                {
                    Menu.SetActive(false);
                    CanvasGroup.alpha = 0;
                }
                break;
            }
            float scale = alphaFadeTime / AlphaFade;
            if (activating)
            {
                scale = 1 - scale;
            }
            CanvasGroup.alpha = scale;
            yield return null;
        }
    }

    public void StartGame()
    {
        CameraController.FollowPlayer();

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
        if (SceneData.Instance.LoadingData != null)
        {
            // Loaded so no give
            return;
        }
        // Gives a battery that lasts 5 minutes
        PlayerInventory.Instance.AddItem(100, new()
        {
            amount = 1,
            float1 = new()
            {
                300
            }
        });
    }

}
