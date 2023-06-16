using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageInteractable : Interactable
{

    public override bool Interact(PlayerInventory playerInventory)
    {
        GarbageManager.Instance.Collected();
        base.Interact(playerInventory);
        return true;
    }

}
