using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public RectTransform RT;

    public GameObject SlotPrefab;

    public List<Slot> Slots;

    public int width;
    public int height;

    public Vector2 size;
    public Vector2 space;
    public Vector2 offset;



    public bool AddItem(int id, int amount = 1)
    {
        foreach (Slot slot in Slots)
        {
            if (slot.item?.id == id)
            {
                slot.item.amount += amount;
                slot.SetDisplay();
                return true;
            }
        }
        foreach (Slot slot in Slots)
        {
            if (slot.item == null)
            {
                slot.item = ItemDatabase.Instance.GetItem(id);
                slot.item.amount = amount;
                slot.SetDisplay();
                return true;
            }
        }
        return false;

    }











    [ContextMenu("Destroy")]
    public void DestroySlots()
    {
        EnsureSlotObjects(0);
    }


    [ContextMenu("Update")]
    public void AssignSlot()
    {
        GetSlots();
        SetSlots();

    }

    [ContextMenu("Set RT Size")]
    public void SetRTSize()
    {
        RT.sizeDelta = new(offset.x + (space.x + size.x) * width, offset.y + (space.y + size.y) * height);
    }

    private void SetSlots()
    {
        int required = width * height;
        EnsureSlotObjects(required);
        int i = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Slots[i].RT.anchoredPosition = new Vector2(offset.x + (space.x + size.x) * x, -offset.y + (space.y + size.y) * -y);
                Slots[i].RT.sizeDelta = size;
                Slots[i].SetDisplay();
                i++;
            }
        }

    }

    private void GetSlots()
    {
        Slots.Clear();
        foreach (Transform tr in transform)
        {
            if (!tr.name.StartsWith("Slot"))
            {
                continue;
            }
            Slots.Add(tr.GetComponent<Slot>());
        }
    }

    private void EnsureSlotObjects(int required)
    {
        while (Slots.Count > required)
        {
            Slot slot = Slots[^1];
            DestroyImmediate(slot.gameObject);
            Slots.RemoveAt(Slots.Count - 1);
        }
        while (Slots.Count < required)
        {
            Slots.Add(CreateSlot());
        }
    }

    private Slot CreateSlot()
    {
        Slot slot = Instantiate(SlotPrefab).GetComponent<Slot>();
        slot.RT.SetParent(transform);
        return slot;
    }

}
