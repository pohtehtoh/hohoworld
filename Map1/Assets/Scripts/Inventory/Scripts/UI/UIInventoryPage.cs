using System;
using Inventory.Model;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
  {
    [SerializeField]
    private GridInventory gridInventory;
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private UIInventoryDescription itemDescription;
    public event Action<UIInventoryItem, InventoryItem> OnDescriptionRequested, OnItemActionRequested;
    [SerializeField]
    private ItemActionPanel actionPanel;

    private void Awake()
    {
      Hide();
      itemDescription.ResetDescription();

      gridInventory = GetComponentInChildren<GridInventory>();
    }
    public void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
      PlacedObject placedObject = inventoryItemUI.gameObject.GetComponent<PlacedObject>();
      InventoryItem inventoryItem = placedObject.GetInventoryItem();
      if (inventoryItem.IsEmpty)
      {
        Debug.Log("Inventory Item is Empty. Returned.");
        return;
      }
      OnItemActionRequested?.Invoke(inventoryItemUI, inventoryItem);
    }
    public void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
      PlacedObject placedObject = inventoryItemUI.gameObject.GetComponent<PlacedObject>();
      InventoryItem inventoryItem = placedObject.GetInventoryItem();
      if (inventoryItem.IsEmpty)
      {
        Debug.Log("Inventory Item is Empty. Returned.");
        return;
  ;    }
      OnDescriptionRequested?.Invoke(inventoryItemUI, inventoryItem);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      ResetSelection();
    }
    public void ResetSelection()
    {
      itemDescription.ResetDescription();
      RemoveItemAction();
    }
    public void AddAction(string actionName, Action performAction)
    {
      actionPanel.AddButton(actionName, performAction);
    }
    public void ShowItemAction(UIInventoryItem inventoryItemUI)
    {
      actionPanel.Toggle(true);
    }
    private void RemoveItemAction()
    {
      actionPanel.Toggle(false);
    }
    public void ResetAllGridItemVisualGrid()
    {
      for (int x = 0; x < gridInventory.GetGrid().GetWidth(); x++)
      {
        for (int y = 0; y < gridInventory.GetGrid().GetHeight(); y++)
        {
          if (gridInventory.GetGrid().GetGridObject(x, y).HasPlacedObject())
          {
            PlacedObject placedObject = gridInventory.GetGrid().GetGridObject(x, y).GetPlacedObject();
            int itemQuantity = placedObject.inventoryItem.quantity;
            gridInventory.RemoveItemAt(placedObject.GetGridPosition());
            gridInventory.TryPlaceItem(placedObject.GetPlacedObjectTypeSO() as ItemSO, placedObject.GetGridPosition(), placedObject.GetDir());
            PlacedObject placedObject2 = gridInventory.GetGrid().GetGridObject(x, y).GetPlacedObject();
            placedObject2.inventoryItem.quantity = itemQuantity;
          }
        }
      }
    }
    public void Hide()
    {
      actionPanel.Toggle(false);
      gameObject.SetActive(false);
    }
    public void UpdateDescription(UIInventoryItem inventoryItemUI, string name, string description)
    {
      itemDescription.SetDescription(name, description);
      RemoveItemAction();
      inventoryItemUI.Select();
    }
  }
}