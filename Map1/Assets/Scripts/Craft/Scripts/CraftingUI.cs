using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    [SerializeField]
    private CraftingRecipeInformation craftingItemDescription;
    [SerializeField]
    private CraftingRecipe currentCraftingRecipe;

    private void Awake()
    {
      Hide();
      craftingItemDescription.Hide();
    }
    public void Show()
    {
      gameObject.SetActive(true);
    }
    public void Hide()
    {
      gameObject.SetActive(false);
      craftingItemDescription.Hide();
    }
    public void WriteRecipeInformation(CraftingRecipe craftingRecipe, Sprite itemImage)
    {
        StringBuilder itemsRequired = new StringBuilder();
        for (int i = 0; i < craftingRecipe.inputItems.Length; i++)
        {
            itemsRequired.Append($"{craftingRecipe.inputItems[i].item.Name}" + " x" + $"{craftingRecipe.inputItems[i].quantity}" + "   ");
        }
        craftingItemDescription.SetDescription(itemImage, craftingRecipe.outputItems.item.Name, craftingRecipe.outputItems.item.Description, itemsRequired.ToString());
        currentCraftingRecipe = craftingRecipe;
    }
    public CraftingRecipe GetCurrentCraftingRecipe()
    {
        return currentCraftingRecipe;
    }
}
