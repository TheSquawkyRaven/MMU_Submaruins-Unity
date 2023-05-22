using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Engine : MonoBehaviour
{

    public PlayerMovement PlayerMovement;

    public Slot Slot;
    public int EngineNumber;

    public int[] EngineItemIDs;

    public Item Item => Slot.Item;
    public ItemData ItemData => Slot.ItemData;

    private void Start()
    {
        Slot.CheckAllowedIDs = true;
        Slot.AllowedIDs = EngineItemIDs;
        Slot.OnItemUpdated += Slot_OnItemUpdated;
    }

    private void Slot_OnItemUpdated(Item item, ItemData itemData)
    {
        if (item == null)
        {
            PlayerMovement.SetEngineBoost(EngineNumber, 1);
            return;
        }
        bool valid = false;
        for (int i = 0; i < EngineItemIDs.Length; i++)
        {
            if (item.id == EngineItemIDs[i])
            {
                valid = true;
                break;
            }
        }
        if (!valid)
        {
            Debug.LogError("INVALID??");
            PlayerMovement.SetEngineBoost(EngineNumber, 1);
            return;
        }

        PlayerMovement.SetEngineBoost(EngineNumber, item.float1);
    }

}
