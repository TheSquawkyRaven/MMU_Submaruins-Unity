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

    public bool IsActive => InventoryCanvas.enabled;

    public event Action InventoryChanged = () => { };

    private void Awake()
    {
        instance = this;
        EnableInventory(false);
    }

    public void InventoryChange()
    {
        InventoryChanged.Invoke();
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
        stored = Toolbar.AddItem(itemID, itemData);
        return stored;
    }

    public void ToggleInventory()
    {
        if (!MainMenu.Instance.Started)
        {
            return;
        }
        EnableInventory(!InventoryCanvas.enabled);
    }

    public void EnableInventory(bool enable)
    {
        if (!enable)
        {
            InventoryCanvas.enabled = false;

            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        InventoryCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public Slot FindItem(int id, bool getFromToolbar)
    {
        Slot slot = Storage.FindItem(id);
        if (slot != null || !getFromToolbar)
        {
            return slot;
        }
        slot = Toolbar.FindItem(id);
        return slot;
    }

}
