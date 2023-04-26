using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public RectTransform RT;

    public GameObject Display;
    public Image Image;
    public TextMeshProUGUI AmountText;

    [NonSerialized] public Item item;

    public void SetDisplay()
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
