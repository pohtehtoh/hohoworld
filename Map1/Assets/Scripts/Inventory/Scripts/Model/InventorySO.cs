using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [field: SerializeField]
        public int Weight = 0;
        [field: SerializeField]
        public int maxWeight = 100;
        [field: SerializeField]
        public int overloadedWeight = 150;
        public GridInventory gridInventory;

        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            if(item.IsStackable == false)
            {
                while (quantity > 0)
                {
                    bool added = AddItemToFirstFreeSlot(item, 1, itemState);
                    if (added)
                    {
                        Weight += item.ItemWeight;
                        return 0;
                    }
                    else
                    {
                        return quantity;
                    }
                }
                return quantity;
            }
            else
            {
                quantity = AddStackableItem(item, quantity);
                return quantity;
            }
            
        }

        private bool AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
            };
            
            bool canAdd = gridInventory.FindFirstFreeGrid(item, newItem);

            if (canAdd)
            {
                item.prefab.GetComponent<PlacedObject>().ChangeInventoryItem(newItem);
            }

            return canAdd;
        }
        private int AddStackableItem(ItemSO item, int quantity)
        {
            RectTransform itemContainer = gridInventory.GetItemContainer();
            foreach (Transform itemPrefab in itemContainer)
            {
                if (itemPrefab.GetComponentInChildren<Image>().sprite == item.prefab.GetComponentInChildren<Image>().sprite)
                {
                    int amountPossibleToTake = itemPrefab.GetComponent<PlacedObject>().inventoryItem.item.MaxStackSize - itemPrefab.GetComponent<PlacedObject>().inventoryItem.quantity;
                    if(quantity > amountPossibleToTake)
                    {
                        Weight += item.ItemWeight * amountPossibleToTake;
                        itemPrefab.GetComponent<PlacedObject>().inventoryItem = itemPrefab.GetComponent<PlacedObject>().inventoryItem.RemoveQuantity(itemPrefab.GetComponent<PlacedObject>().inventoryItem.item.MaxStackSize);
                        itemPrefab.GetComponent<PlacedObject>().ChangeQuantityText();
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        Weight += item.ItemWeight * quantity;
                        itemPrefab.GetComponent<PlacedObject>().inventoryItem = itemPrefab.GetComponent<PlacedObject>().inventoryItem.AddQuantity(itemPrefab.GetComponent<PlacedObject>().inventoryItem.quantity + quantity);
                        itemPrefab.GetComponent<PlacedObject>().ChangeQuantityText();
                        return 0;
                    }
                }
            }
            while (quantity > 0)
            {
                Weight += item.ItemWeight * quantity;
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }
        public InventoryItem RemoveItem(InventoryItem inventoryItem, int amount)
        {
            if(inventoryItem.IsEmpty)
                return inventoryItem;
            int reminder = inventoryItem.quantity - amount;
            Weight -= inventoryItem.item.ItemWeight * amount;
            if(Weight < 0) Weight = 0;
            if(reminder <= 0)
                inventoryItem = InventoryItem.GetEmptyItem();
            else
                inventoryItem = inventoryItem.RemoveQuantity(reminder);
            return inventoryItem;
        }
    }
    
    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;

        public InventoryItem AddQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public InventoryItem RemoveQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem
        {
            item = null,
            quantity = 0,
            itemState = new List<ItemParameter>()
        };
    }
}