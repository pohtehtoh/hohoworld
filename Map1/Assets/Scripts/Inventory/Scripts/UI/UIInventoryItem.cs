using System;
using Inventory.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler/*, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler*/
    {
        private UIInventoryPage inventoryPage;
        private GridInventory gridInventory;
        private PlacedObject placedObject;
        public event Action<UIInventoryItem> OnItemClicked;

        private void Start()
        {
            inventoryPage = this.gameObject.GetComponentInParent<UIInventoryPage>();
            placedObject = GetComponent<PlacedObject>();

            OnItemClicked += inventoryPage.HandleItemSelection;
            OnItemClicked += inventoryPage.HandleShowItemActions;
        }
        public void Select()
        {
            ItemSO.CreateVisualGrid(transform.GetChild(0), placedObject.GetPlacedObjectTypeSO() as ItemSO, gridInventory.GetGrid().GetCellSize());
            for (int x = 0; x < gridInventory.GetGrid().GetWidth(); x++)
            {
                for (int y = 0; y < gridInventory.GetGrid().GetHeight(); y++)
                {
                    if (gridInventory.GetGrid().GetGridObject(x, y).HasPlacedObject())
                    {
                        PlacedObject placedObject = gridInventory.GetGrid().GetGridObject(x, y).GetPlacedObject();
                        if (this.placedObject != placedObject)
                        {
                            gridInventory.RemoveItemAt(placedObject.GetGridPosition());
                            gridInventory.TryPlaceItem(placedObject.GetPlacedObjectTypeSO() as ItemSO, placedObject.GetGridPosition(), placedObject.GetDir());
                        }
                    }
                }
            }
        }
        public void OnPointerClick(PointerEventData pointerData)
        {
            OnItemClicked?.Invoke(this);
        }
        internal void Setup(GridInventory gridInventory)
        {
            this.gridInventory = gridInventory;
        }
    }
}