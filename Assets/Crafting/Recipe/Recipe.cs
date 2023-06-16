using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{

    [System.Serializable]
    public class RecipeUI
    {
        public GameObject Obj;
        public Image Image;
        public TextMeshProUGUI Text;
    }

    public TextMeshProUGUI RecipeTitleText;
    public Button CraftButton;

    public RecipeUI[] RecipeUIs;

    private Crafting.RecipeData RecipeData;

    public float BatteryFloatAmount;

    public void SetRecipe(Crafting.RecipeData data)
    {
        RecipeData = data;
        Item item = ItemDatabase.Instance.GetItem(data.ResultID);
        RecipeTitleText.SetText(item.name);

        int i = 0;
        for (; i < data.IngredientIDs.Length; i++)
        {
            Item ingredient = ItemDatabase.Instance.GetItem(data.IngredientIDs[i]);
            RecipeUIs[i].Image.sprite = ingredient.sprite;
            RecipeUIs[i].Text.SetText(data.IngredientAmount[i].ToString());
            RecipeUIs[i].Obj.SetActive(true);
        }
        for (; i < RecipeUIs.Length; i++)
        {
            RecipeUIs[i].Obj.SetActive(false);
        }

        CraftButton.onClick.AddListener(Craft);
        PlayerInventory.Instance.InventoryChanged += Instance_InventoryChanged;

    }

    private void Instance_InventoryChanged()
    {
        Slot[] slots = CanCraft();
        if (slots == null)
        {
            CraftButton.interactable = false;
        }
        else
        {
            CraftButton.interactable = true;
        }
    }

    public Slot[] CanCraft()
    {
        Slot[] slots = new Slot[RecipeData.IngredientIDs.Length];
        for (int i = 0; i < RecipeData.IngredientIDs.Length; i++)
        {
            Slot slot = PlayerInventory.Instance.FindItem(RecipeData.IngredientIDs[i]);
            if (slot == null)
            {
                // Item not found
                return null;
            }

            if (slot.ItemData.amount < RecipeData.IngredientAmount[i])
            {
                // Not enough
                return null;
            }
            slots[i] = slot;
        }
        return slots;
    }

    public void Craft()
    {
        Slot[] slots = CanCraft();
        if (slots == null)
        {
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveAmount(RecipeData.IngredientAmount[i]);
        }
        PlayerInventory.Instance.AddItem(RecipeData.ResultID, new()
        {
            amount = 1,
            float1 = new List<float>() { RecipeData.ResultID == 100 ? BatteryFloatAmount : 0 }
        });
    }

}
