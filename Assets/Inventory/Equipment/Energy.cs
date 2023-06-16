using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Energy : MonoBehaviour
{

    public Slot Slot;
    public TextMeshProUGUI EnergyRemainingText;
    public TextMeshProUGUI EnergyRemainingTextHUD;
    public int EnergyItemID;

    public Item Item => Slot.Item;
    public ItemData ItemData => Slot.ItemData;

    private bool timeRemainingZero;
    public float TimeRemaining { get; set; }

    private void Start()
    {
        Slot.CheckAllowedIDs = true;
        Slot.AllowedIDs = new int[] { EnergyItemID };
    }

    private void Update()
    {
        if (!MainMenu.Instance.Started)
        {
            return;
        }
        CheckSlotEnergy();
        UpdateText();
    }

    private void CheckSlotEnergy()
    {
        if (Item == null)
        {
            TimeRemaining = 0;
            NoTimeRemaining();
            return;
        }
        if (Item.id != EnergyItemID)
        {
            TimeRemaining = 0;
            NoTimeRemaining();
            return;
        }
        EnergyUpdate(ItemData);
    }

    private void EnergyUpdate(ItemData itemData)
    {
        if (itemData.float1.Count == 0)
        {
            Depleted();
            return;
        }

        itemData.float1[0] -= Time.deltaTime;
        if (itemData.float1[0] <= 0)
        {
            itemData.amount -= 1;
            itemData.float1.RemoveAt(0);

            if (itemData.amount == 0)
            {
                Depleted();
            }
        }

        if (itemData.float1.Count == 0)
        {
            TimeRemaining = 0;
            NoTimeRemaining();
            return;
        }
        float total = 0;
        for (int i = 0; i < itemData.float1.Count; i++)
        {
            total += itemData.float1[i];
        }
        TimeRemaining = total;

    }

    private void UpdateText()
    {
        int minutes = (int)(TimeRemaining / 60);
        int seconds = (int)(TimeRemaining % 60);
        string minutesT = minutes < 10 ? "0" + minutes : minutes.ToString();
        string secondsT = seconds < 10 ? "0" + seconds : seconds.ToString();

        string text = $"Energy Remaining:\n{minutesT}:{secondsT}";
        EnergyRemainingText.SetText(text);
        EnergyRemainingTextHUD.SetText(text);
    }

    private void Depleted()
    {
        Slot.SetItem(null, null);
        Slot.SetDisplay();
    }

    private void NoTimeRemaining()
    {
        Slot slot = PlayerInventory.Instance.FindItem(EnergyItemID);
        if (slot != null)
        {
            slot.TransferToSlot(Slot);
            return;
        }

        if (timeRemainingZero)
        {
            return;
        }
        if (!timeRemainingZero)
        {
            timeRemainingZero = true;
        }
        Debug.LogWarning("No Battery Remaining");
    }



}
