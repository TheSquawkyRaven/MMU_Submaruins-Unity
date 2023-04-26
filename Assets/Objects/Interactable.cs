using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public string Name;
    public int itemID;


    public void Interact(PlayerInventory playerInventory)
    {
        if (playerInventory.AddItem(itemID))
        {
            Destroy(gameObject);
            return;
        }
        //Full
    }


}
