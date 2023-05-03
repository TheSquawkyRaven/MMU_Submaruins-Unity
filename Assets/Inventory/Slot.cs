using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        Item holdingItem = HoldingItem.Instance.Item();
        if (holdingItem == null)
        {
            if (item == null)
            {
                SetDisplay();
                return;
            }
            HoldingItem.Instance.Slot_SetItem(item);
            item = null;
            SetDisplay();
            return;
        }
        if (item != null)
        {
            HoldingItem.Instance.Slot_SetItem(item);
        }
        item = holdingItem;
        HoldingItem.Instance.Slot_ReceiveItem();
        SetDisplay();
    }

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
            AmountText.SetText(item.amount.ToString());
        }
    }


}
