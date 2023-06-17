using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static PlayerInventory instance;
    public static PlayerInventory Instance => instance;

    public Canvas InventoryCanvas;
    public Inventory Equipment;
    public Inventory Storage;
    public Inventory Toolbar;

    public CanvasGroup CG;

    public bool IsActive => InventoryCanvas.enabled;

    public event Action InventoryChanged = () => { };

    private float alphaFadeTime;
    public float AlphaFade;
    private void Awake()
    {
        instance = this;
        EnableInventory(false);
        InventoryCanvas.enabled = false;
    }

    public void InventoryChange()
    {
        InventoryChanged.Invoke();

        if (GameSaveLoader.Instance != null)
        {
            GameSaveLoader.Instance.SaveGame();
        }
    }

    public bool AddItem(int itemID, ItemData itemData)
    {
        // Uncomment if items go directly into equipment
        //bool stored = Equipment.MatchAddItem(itemID, itemData);
        //if (stored)
        //{
        //    return true;
        //}
        bool stored = Storage.AddItem(itemID, itemData);
        if (stored)
        {
            return true;
        }
        return stored;
    }

    public void ToggleInventory()
    {
        if (!ToggleMenu.Instance.Started)
        {
            return;
        }
        EnableInventory(!InventoryCanvas.enabled);
    }

    public IEnumerator LerpCanvasAlpha(bool activating)
    {
        InventoryCanvas.enabled = true;
        alphaFadeTime = AlphaFade;
        while (true)
        {
            alphaFadeTime -= Time.unscaledDeltaTime;
            if (alphaFadeTime <= 0)
            {
                if (activating)
                {
                    CG.alpha = 1;
                }
                else
                {
                    InventoryCanvas.enabled = false;
                    CG.alpha = 0;
                }
                break;
            }
            float scale = alphaFadeTime / AlphaFade;
            if (activating)
            {
                scale = 1 - scale;
            }
            CG.alpha = scale;
            yield return null;
        }
    }
    public void EnableInventory(bool enable)
    {
        if (!enable)
        {
            StartCoroutine(LerpCanvasAlpha(false));
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        StartCoroutine(LerpCanvasAlpha(true));
        Cursor.lockState = CursorLockMode.None;
    }

    public Slot FindItem(int id)
    {
        Slot slot = Storage.FindItem(id);
        return slot;
    }


    [ContextMenu("Add Stuff")]
    public void AddStuff()
    {
        AddItem(0, new() { amount = 50 });
        AddItem(1, new() { amount = 50 });
    }

}
