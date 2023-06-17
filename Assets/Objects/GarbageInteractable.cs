using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageInteractable : Interactable
{

    public bool IsMetal;

    public int index;

    public override bool Interact(PlayerInventory playerInventory)
    {
        GarbageManager.Instance.Collected(this, IsMetal);
        base.Interact(playerInventory);
        Debug.Log("Update Score");
        Score.Instance.UpdateScore();
        return true;
    }

}
