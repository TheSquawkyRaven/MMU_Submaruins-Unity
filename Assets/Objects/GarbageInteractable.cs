using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageInteractable : Interactable
{

    public override bool Interact(PlayerInventory playerInventory)
    {
        itemID = GarbageManager.Instance.ItemIDs[Random.Range(0, GarbageManager.Instance.ItemIDs.Length)];
        itemData.amount = 1;
        GarbageManager.Instance.Collected();
        base.Interact(playerInventory);
        return true;
    }

}
