using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public Inventory Storage;
    public Inventory Toolbar;

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



}
