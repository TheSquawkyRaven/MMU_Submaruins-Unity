using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    private static ItemDatabase instance;
    public static ItemDatabase Instance => instance;

    public Item[] AllItems;
    public readonly Dictionary<int, Item> IDToItem = new();

    private void Awake()
    {
        instance = this;

        IDToItem.Clear();
        foreach (var item in AllItems)
        {
            IDToItem.Add(item.id, item);
        }
    }

    public Item GetItem(int id)
    {
        return IDToItem[id];
    }


}
