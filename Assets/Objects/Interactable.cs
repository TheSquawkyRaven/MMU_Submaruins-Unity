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

    public virtual bool Interact(PlayerInventory playerInventory)
    {
        if (playerInventory.AddItem(itemID, itemData))
        {
            CallOnDestroy();
            Destroy(gameObject);
            return true;
        }
        //Full
        return false;
    }

    protected void CallOnDestroy()
    {
        OnDestroy.Invoke(this);
    }


}
