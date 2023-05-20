using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public string Name;
    public int itemID;

    public ItemData itemData;

    public event Action<Interactable> OnDestroy = _ => { };

    public void Interact(PlayerInventory playerInventory)
    {
        if (playerInventory.AddItem(itemID, itemData))
        {
            OnDestroy.Invoke(this);
            Destroy(gameObject);
            return;
        }
        //Full
    }


}
