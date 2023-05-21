using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldingItem : Slot
{
    private static HoldingItem instance;
    public static HoldingItem Instance => instance;

    public GameObject HoldingItemObject;

    private void Awake()
    {
        instance = this;
        SetDisplay();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        
    }

    private void Update()
    {
        if (HoldingItemObject.activeSelf)
        {
            RT.anchoredPosition = Input.mousePosition;
        }
    }

    public Item Item()
    {
        return item;
    }
    public ItemData ItemData()
    {
        return itemData;
    }

    public override void ReceiveItem(Item item, ItemData itemData)
    {
        base.ReceiveItem(item, itemData);
        HoldingItemObject.SetActive(true);
    }

    public override void TransferToSlot(Slot other)
    {
        base.TransferToSlot(other);
        HoldingItemObject.SetActive(false);
    }


}
