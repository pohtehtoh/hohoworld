using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;

[CreateAssetMenu(fileName = "newCraftingRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public InventoryItem[] inputItems;
    public InventoryItem outputItems;
    public GameObject outputItemPrefab;

    public bool CanCraft(InventoryController inventoryController)
    {
        for (int i = 0; i < inputItems.Length; i++)
        {
            if (!inventoryController.gridInventory.Contains(inputItems[i], inputItems[i].quantity))
                return false;
        }
        
        return true;
    }
    public void Craft(InventoryController inventoryController)
    {
        for (int i = 0; i < inputItems.Length; i++)
        {
            inventoryController.inventoryData.RemoveItem(inputItems[i], inputItems[i].quantity);
            inventoryController.gridInventory.RemoveItemAt(inventoryController.gridInventory.Contains(inputItems[i]).GetGridPosition());
            for (int j = 0; j < inputItems[i].quantity; j++)
            {
                Destroy(inventoryController.GetGameObject(inputItems[i].item.Name));
            }
        }

        inventoryController.inventoryData.AddItem(outputItems.item, outputItems.quantity, outputItems.itemState);
        Instantiate(outputItemPrefab, GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSwitch>().gameObject.transform);
        outputItemPrefab.GetComponent<Item>().PickUpItem();
    }
}
