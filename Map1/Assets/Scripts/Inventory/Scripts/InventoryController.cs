using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Model;
using Inventory.UI;
using TMPro;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour, IDataPersistence
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;
        [SerializeField]
        public InventorySO inventoryData;
        [SerializeField]
        public GridInventory gridInventory;
        [SerializeField]
        public PickUpSystem pickUpSystem;
        [SerializeField]
        public GridInventoryAssets gridInventoryAssets;
        [SerializeField]
        private TMP_Text weight;
        public List<InventoryItem> initialItems = new List<InventoryItem>();
        [SerializeField]
        public GameObject[] interactables;
        [SerializeField]
        private AudioClip dropClip;
        [SerializeField]
        private AudioSource audioSource;
        private InventoryAction inventoryInput;
        public static InventoryController instance { get; private set; }

        public void Craft(CraftingRecipe craftingRecipe)
        {
            bool bagFull = true;
            for (int y = gridInventory.GetGrid().GetHeight(); y >= 0; y--)
            {
                for (int x = 0; x < gridInventory.GetGrid().GetWidth(); x++)
                {
                    if (gridInventory.CanPlaceItem(craftingRecipe.outputItems.item, gridInventory.GetGridPosition(gridInventory.GetGrid().GetWorldPosition(x, y)), PlacedObjectTypeSO.Dir.Up))
                    {
                        bagFull = false;
                        break;
                    }
                    
                }
            }

            if(!bagFull)
            {
                if (craftingRecipe.CanCraft(this))
                {
                    craftingRecipe.Craft(this);
                    string text = "x" + $"{craftingRecipe.outputItems.quantity}" + " " + $"{craftingRecipe.outputItems.item.Name}" + " created";
                    pickUpSystem.AddActionText(text, Color.white);
                }
                else
                {
                    pickUpSystem.AddActionText("Not enough materials", Color.red);
                }
            }
            else
            {
                pickUpSystem.AddActionText("Bag full", Color.red);
            }
        }
    
        private void Awake()
        {
            instance = this;
            inventoryInput = new InventoryAction();

            gridInventory.InitializeGrid();
        }
        private void OnEnable()
        {
            inventoryInput.Enable();
        }
        private void OnDisable()
        {
            inventoryInput.Disable();
        }
        public void Start()
        {
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;

            inventoryData.gridInventory = this.gridInventory;

            pickUpSystem = GetComponent<PickUpSystem>();

            interactables = GameObject.FindGameObjectsWithTag("Selectable");
        }
        public void InitializeInitialItems()
        {
            foreach (InventoryItem item in initialItems)
            {
                if(item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }
        private void HandleItemActionRequest(UIInventoryItem inventoryItemUI, InventoryItem inventoryItem)
        {
            if(inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if(itemAction != null)
            {
                inventoryUI.ShowItemAction(inventoryItemUI);

                if (GetGameObject(inventoryItem.item.Name) != null)
                {
                    if (GetGameObject(inventoryItem.item.Name).GetComponent<Item>().useOtherAction)
                    {
                    inventoryUI.AddAction(itemAction.ActionName2, () => PerformAction(inventoryItemUI, inventoryItem));
                    }
                    else
                    {
                        inventoryUI.AddAction(itemAction.ActionName1, () => PerformAction(inventoryItemUI, inventoryItem));
                    }

                    // if (GetGameObject(inventoryItem.item.Name).GetComponent<Item>().itemType == Item.ItemType.weapon)
                    // {
                    //     if (GetGameObject(inventoryItem.item.Name).GetComponent<Item>().useOtherAction)
                    //     {
                    //     inventoryUI.AddAction(itemAction.ActionName2, () => PerformAction(inventoryItemUI, inventoryItem));
                    //     }
                    //     else
                    //     {
                    //         inventoryUI.AddAction(itemAction.ActionName1, () => PerformAction(inventoryItemUI, inventoryItem));
                    //     }
                    // }
                    // else if(GetGameObject(inventoryItem.item.Name).GetComponent<Item>().itemType == Item.ItemType.item)
                    // {
                    //     // if (Vector3.Distance(this.transform.position, keyLock.gameObject.transform.position) <= 3)
                    //     // {
                    //     //     inventoryUI.AddAction(itemAction.ActionName1, () => PerformAction(inventoryItemUI, inventoryItem));
                    //     // }
                    // }
                    // else
                    // {
                    //     inventoryUI.AddAction(itemAction.ActionName1, () => PerformAction(inventoryItemUI, inventoryItem));
                    // }
                }
                // else
                // {
                //     inventoryUI.AddAction(itemAction.ActionName1, () => PerformAction(inventoryItemUI, inventoryItem));
                // }
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if(destroyableItem != null)
            {
                inventoryUI.AddAction("Drop", () => DropItem(/*itemIndex, */inventoryItemUI, inventoryItem));
            }
        }
        private void DropItem(UIInventoryItem inventoryItemUI, InventoryItem inventoryItem)
        {
            inventoryItemUI.GetComponent<PlacedObject>().ChangeInventoryItem(inventoryData.RemoveItem(inventoryItem, 1));
            if(inventoryItemUI.GetComponent<PlacedObject>().GetInventoryItem().IsEmpty) gridInventory.RemoveItemAt(inventoryItemUI.GetComponent<PlacedObject>().GetGridPosition());

            bool hasItem = gridInventory.GetGrid().GetGridObject(inventoryItemUI.GetComponent<PlacedObject>().GetGridPosition().x, inventoryItemUI.GetComponent<PlacedObject>().GetGridPosition().y).HasPlacedObject();
            if (!hasItem) inventoryUI.ResetSelection();

            audioSource.PlayOneShot(dropClip);

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if(itemAction != null)
            {
                if (GetGameObject(inventoryItem.item.Name) != null)
                {
                    if (GetGameObject(inventoryItem.item.Name).GetComponent<Item>().itemType == Item.ItemType.weapon && GetGameObject(inventoryItem.item.Name).GetComponent<Item>().useOtherAction)
                    {
                        itemAction.PerformActionTwo(gameObject, inventoryItem);
                    }
                }
            }

            if (GetGameObject(inventoryItem.item.Name) != null)
            {
                GetGameObject(inventoryItem.item.Name).GetComponent<Item>().DropItem(inventoryItem);
            }
        }
        public void PerformAction(UIInventoryItem inventoryItemUI, InventoryItem inventoryItem)
        {
            if(inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if(destroyableItem != null)
            {
                if(GetGameObject(inventoryItem.item.Name) != null)
                {
                    if (GetGameObject(inventoryItem.item.Name).GetComponent<Item>().itemType == Item.ItemType.food || GetGameObject(inventoryItem.item.Name).GetComponent<Item>().itemType == Item.ItemType.item)
                    {
                        if(itemAction != null)
                        {
                            if(itemAction.PerformActionOne(gameObject, inventoryItem))
                            {
                                audioSource.PlayOneShot(itemAction.action1SFX);
                                inventoryItemUI.GetComponent<PlacedObject>().ChangeInventoryItem(inventoryData.RemoveItem(inventoryItem, 1));
                                if(inventoryItemUI.GetComponent<PlacedObject>().GetInventoryItem().IsEmpty) gridInventory.RemoveItemAt(inventoryItemUI.GetComponent<PlacedObject>().GetGridPosition());
                                bool hasItem = gridInventory.GetGrid().GetGridObject(inventoryItemUI.GetComponent<PlacedObject>().GetGridPosition().x, inventoryItemUI.GetComponent<PlacedObject>().GetGridPosition().y).HasPlacedObject();
                                if (!hasItem) inventoryUI.ResetSelection();
                                Destroy(GetGameObject(inventoryItem.item.Name));
                            }
                        }
                    }
                    else
                    {
                        if(itemAction != null)
                        {
                            if (GetGameObject(inventoryItem.item.Name) != null)
                            {
                                if (/*GetGameObject(inventoryItem.item.Name).GetComponent<Item>().itemType == Item.ItemType.weapon && */GetGameObject(inventoryItem.item.Name).GetComponent<Item>().useOtherAction)
                                {
                                    itemAction.PerformActionTwo(gameObject, inventoryItem);
                                    audioSource.PlayOneShot(itemAction.action2SFX);
                                }
                                else
                                {
                                    itemAction.PerformActionOne(gameObject, inventoryItem);
                                    audioSource.PlayOneShot(itemAction.action1SFX);
                                }
                            }
                            inventoryUI.ResetSelection();
                        }
                    }
                }
            }
        }
        private void HandleDescriptionRequest(UIInventoryItem inventoryItemUI, InventoryItem inventoryItem)
        {
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(inventoryItemUI, item.name, description);
        }
        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            sb.Append($"Weight: " + $"{inventoryItem.item.ItemWeight}");
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " + $": {inventoryItem.itemState[i].value} / " + $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public void Update()
        {
            weight.text = " " + inventoryData.Weight;
            if (inventoryInput.InventoryMain.Bag.triggered)
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
        }
        public GameObject GetGameObject(string name)
        {
            WeaponSwitch weaponSwitch = GetComponentInChildren<WeaponSwitch>();
            foreach(Transform item in weaponSwitch.transform)
            {
                if (item.gameObject.GetComponent<Item>() != null)
                {
                    if (item.gameObject.GetComponent<Item>().InventoryItem.Name == name)
                    {
                        return item.gameObject;
                    }
                }
            }
            return null;
        }
        public void LoadData(GameData data)
        {
            ListAddItem listAddItem = data.listAddItem;
            foreach (AddItem addItem in listAddItem.addItemList)
            {
                gridInventory.TryPlaceItem(gridInventoryAssets.GetItemSOFromName(addItem.itemSOName), addItem.gridPosition, addItem.dir);
            }

            this.inventoryData.Weight = data.inventoryWeight;
        }

        public void SaveData(GameData data)
        {
            List<PlacedObject> placedObjectList = new List<PlacedObject>();
            for (int x = 0; x < gridInventory.GetGrid().GetWidth(); x++)
            {
                for (int y = 0; y < gridInventory.GetGrid().GetHeight(); y++)
                {
                    if (gridInventory.GetGrid().GetGridObject(x, y).HasPlacedObject())
                    {
                        placedObjectList.Remove(gridInventory.GetGrid().GetGridObject(x, y).GetPlacedObject());
                        placedObjectList.Add(gridInventory.GetGrid().GetGridObject(x, y).GetPlacedObject());
                    }
                }
            }
            List<AddItem> addItemList = new List<AddItem>();
            foreach (PlacedObject placedObject in placedObjectList)
            {
                addItemList.Add(new AddItem
                    {
                        dir = placedObject.GetDir(),
                        gridPosition = placedObject.GetGridPosition(),
                        itemSOName = (placedObject.GetPlacedObjectTypeSO() as ItemSO).name,
                    });
            }
            data.listAddItem = new ListAddItem
            { 
                addItemList = addItemList 
            };

            data.inventoryWeight = this.inventoryData.Weight;
        }

        [Serializable]
        public struct AddItem
        {
        public string itemSOName;
        public Vector2Int gridPosition;
        public PlacedObjectTypeSO.Dir dir;
        }

        [Serializable]
        public struct ListAddItem
        {
            public List<AddItem> addItemList;
        }
    }
}