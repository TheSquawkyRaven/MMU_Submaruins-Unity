using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingItem : MonoBehaviour
{
    private static HoldingItem instance;
    public static HoldingItem Instance => instance;

    [NonSerialized] public Item item;

    private void Awake()
    {
        instance = this;
    }


}
