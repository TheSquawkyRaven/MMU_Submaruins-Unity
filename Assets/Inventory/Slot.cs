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

    [NonSerialized] public Item item;
    [NonSerialized] public ItemData itemData;
    public bool cannotBeTaken = false;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Item holdingItem = HoldingItem.Instance.Item();
        ItemData holdingItemData = HoldingItem.Instance.ItemData();

        if (cannotBeTaken)
        {
            if (holdingItem == null)
            {
                return;
            }
            if (item.id == holdingItem.id)
            {
                itemData.Add(holdingItemData);
                HoldingItem.Instance.Slot_ReceiveItem();
                SetDisplay();
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
            HoldingItem.Instance.Slot_SetItem(item, itemData);
            item = null;
            itemData = null;
            SetDisplay();
            return;
        }
        if (item != null)
        {
            HoldingItem.Instance.Slot_SetItem(item, itemData);
        }
        item = holdingItem;
        itemData = holdingItemData;
        HoldingItem.Instance.Slot_ReceiveItem();
        SetDisplay();
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

    public void TransferToSlot(Slot other)
    {
        other.item = item;
        other.itemData = itemData;
        item = null;
        itemData = null;
        SetDisplay();
        other.SetDisplay();
    }


}
