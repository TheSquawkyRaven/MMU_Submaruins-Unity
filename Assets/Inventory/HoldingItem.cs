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

    public override void SetItem(Item item, ItemData itemData)
    {
        base.SetItem(item, itemData);
        HoldingItemObject.SetActive(item != null);
    }


}
