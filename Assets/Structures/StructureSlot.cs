using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSlot : MonoBehaviour
{

    public Slot Slot;

    public int StructureItemID;

    private void Start()
    {
        Slot.CheckAllowedIDs = true;
        Slot.AllowedIDs = new int[1] { StructureItemID };
        PlayerInventory.Instance.InventoryChanged += Instance_InventoryChanged;
    }

    private void Instance_InventoryChanged()
    {
        Slot slot = PlayerInventory.Instance.FindItem(StructureItemID);
        if (slot == null)
        {
            return;
        }
        Item item = slot.Item;
        ItemData itemData = slot.ItemData;
        slot.SetItem(null, null);
        if (Slot.Item != null)
        {
            Slot.ItemData.amount += itemData.amount;
            Slot.SetDisplay();
        }
        else
        {
            Slot.SetItem(item, itemData);
        }
    }

}
