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

    public void Slot_SetItem(Item item)
    {
        this.item = item;
        SetDisplay();
        HoldingItemObject.SetActive(true);
    }
    public void Slot_ReceiveItem()
    {
        item = null;
        SetDisplay();
        HoldingItemObject.SetActive(false);
    }


}
