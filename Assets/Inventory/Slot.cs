using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{

    public RectTransform RT;

    public GameObject Display;
    public Image Image;
    public TextMeshProUGUI AmountText;

    [NonSerialized] private Item item;
    [NonSerialized] private ItemData itemData;

    public Item Item => item;
    public ItemData ItemData => itemData;

    public event Action<Item, ItemData> OnItemUpdated = (_, _) => { };

    public bool cannotBeTaken = false;
    public bool CheckAllowedIDs = false;
    public int[] AllowedIDs { get; set; } = new int[0];

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Item holdingItem = HoldingItem.Instance.Item;
        ItemData holdingItemData = HoldingItem.Instance.ItemData;


        if (cannotBeTaken)
        {
            if (holdingItem == null)
            {
                return;
            }
            if (item == null)
            {
                if (!Allowed(holdingItem.id))
                {
                    return;
                }

                HoldingItem.Instance.SetItem(null, null);
                SetItem(holdingItem, holdingItemData);
                return;
            }
            if (item.id == holdingItem.id)
            {
                AddItemData(holdingItemData);
                HoldingItem.Instance.SetItem(null, null);
                return;
            }
            return;
        }

        if (holdingItem == null)
        {
            if (item == null)
            {
                SetDisplay();
                return;
            }
            
            HoldingItem.Instance.SetItem(item, itemData);
            SetItem(null, null);
            return;
        }

        if (!Allowed(holdingItem.id))
        {
            return;
        }


        if (item != null)
        {
            HoldingItem.Instance.SetItem(item, itemData);
        }
        else
        {
            HoldingItem.Instance.SetItem(null, null);
        }
        SetItem(holdingItem, holdingItemData);
    }

    public bool Allowed(int itemID)
    {
        if (!CheckAllowedIDs)
        {
            return true;
        }
        if (CheckAllowedIDs)
        {
            for (int i = 0; i < AllowedIDs.Length; i++)
            {
                if (itemID == AllowedIDs[i])
                {
                    return true;
                }
            }
        }
        return false;
    }

    [ContextMenu("Set Display")]
    public virtual void SetDisplay()
    {
        if (item == null)
        {
            Display.SetActive(false);
        }
        else
        {
            Display.SetActive(true);
            Image.sprite = item.sprite;
            AmountText.SetText(itemData.amount.ToString());
        }
    }

    public void SetCannotBeTaken()
    {

    }

    public virtual void TransferToSlot(Slot other)
    {
        other.SetItem(item, itemData);
        SetItem(null, null);
    }

    public virtual void SetItem(Item item, ItemData itemData)
    {
        this.item = item;
        this.itemData = itemData;
        ItemUpdate();
    }
    public virtual void AddItemData(ItemData itemData)
    {
        this.itemData.Add(itemData);
        ItemUpdate();
    }

    public virtual void ItemUpdate()
    {
        SetDisplay();
        OnItemUpdated.Invoke(Item, ItemData);
        PlayerInventory.Instance.InventoryChange();
    }

    public virtual void RemoveAmount(int amount)
    {
        itemData.amount -= amount;
        if (itemData.amount <= 0)
        {
            SetItem(null, null);
        }
        else
        {
            ItemUpdate();
        }
    }


}
