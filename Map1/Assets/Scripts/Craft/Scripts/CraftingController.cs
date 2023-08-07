using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    [SerializeField]
    public CraftingRecipeInformation craftingItemDescription;
    [SerializeField]
    public CraftingUI craftingUI;
    
    public void Craft()
    {
        // if(craftingUI.GetCurrentCraftingRecipe().CanCraft(GetComponent<InventoryController>()))
        // {
        //     craftingUI.GetCurrentCraftingRecipe().Craft(GetComponent<InventoryController>());
        // }
        GetComponent<InventoryController>().Craft(craftingUI.GetCurrentCraftingRecipe());
    }
}
