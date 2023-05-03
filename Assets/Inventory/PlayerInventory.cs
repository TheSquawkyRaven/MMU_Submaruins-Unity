using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static PlayerInventory instance;
    public static PlayerInventory Instance => instance;

    public GameObject InventoryObject;
    public Inventory Storage;
    public Inventory Toolbar;

    public bool IsActive => InventoryObject.activeSelf;

    private void Awake()
    {
        instance = this;
        EnableInventory(false);
    }

    public bool AddItem(int itemID)
    {
        bool stored = Storage.AddItem(itemID);
        if (stored)
        {
            return true;
        }
        stored = Toolbar.AddItem(itemID);
        return stored;
    }

    public void ToggleInventory()
    {
        EnableInventory(!InventoryObject.activeSelf);
    }

    public void EnableInventory(bool enable)
    {
        if (!enable)
        {
            InventoryObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        InventoryObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }


}
