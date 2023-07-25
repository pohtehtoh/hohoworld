using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class GridInventoryDragDropSystem : MonoBehaviour {

    public static GridInventoryDragDropSystem Instance { get; private set; }



    [SerializeField] private List<GridInventory> gridInventoryList;

    private GridInventory draggingInventory;
    private PlacedObject draggingPlacedObject;
    private Vector2Int mouseDragGridPositionOffset;
    private Vector2 mouseDragAnchoredPositionOffset;
    private PlacedObjectTypeSO.Dir dir;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        foreach (GridInventory gridInventory in gridInventoryList) {
            gridInventory.OnObjectPlaced += (object sender, PlacedObject placedObject) => {

            };
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
        }

        if (draggingPlacedObject != null) {
            // Calculate target position to move the dragged item
            RectTransformUtility.ScreenPointToLocalPointInRectangle(draggingInventory.GetItemContainer(), Input.mousePosition, null, out Vector2 targetPosition);
            targetPosition += new Vector2(-mouseDragAnchoredPositionOffset.x, -mouseDragAnchoredPositionOffset.y);

            // Apply rotation offset to target position
            Vector2Int rotationOffset = draggingPlacedObject.GetPlacedObjectTypeSO().GetRotationOffset(dir);
            targetPosition += new Vector2(rotationOffset.x, rotationOffset.y) * draggingInventory.GetGrid().GetCellSize();

            // Snap position
            targetPosition /= 10f;// draggingInventoryTetris.GetGrid().GetCellSize();
            targetPosition = new Vector2(Mathf.Floor(targetPosition.x), Mathf.Floor(targetPosition.y));
            targetPosition *= 10f;

            // Move and rotate dragged object
            draggingPlacedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(draggingPlacedObject.GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 20f);
            draggingPlacedObject.transform.rotation = Quaternion.Lerp(draggingPlacedObject.transform.rotation, Quaternion.Euler(0, 0, -draggingPlacedObject.GetPlacedObjectTypeSO().GetRotationAngle(dir)), Time.deltaTime * 15f);
        }
    }

    public void StartedDragging(GridInventory gridInventory, PlacedObject placedObject) {
        // Started Dragging
        draggingInventory = gridInventory;
        draggingPlacedObject = placedObject;

        Cursor.visible = false;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(gridInventory.GetItemContainer(), Input.mousePosition, null, out Vector2 anchoredPosition);
        Vector2Int mouseGridPosition = gridInventory.GetGridPosition(anchoredPosition);

        // Calculate Grid Position offset from the placedObject origin to the mouseGridPosition
        mouseDragGridPositionOffset = mouseGridPosition - placedObject.GetGridPosition();

        // Calculate the anchored poisiton offset, where exactly on the image the player clicked
        mouseDragAnchoredPositionOffset = anchoredPosition - placedObject.GetComponent<RectTransform>().anchoredPosition;

        // Save initial direction when started draggign
        dir = placedObject.GetDir();

        // Apply rotation offset to drag anchored position offset
        Vector2Int rotationOffset = draggingPlacedObject.GetPlacedObjectTypeSO().GetRotationOffset(dir);
        mouseDragAnchoredPositionOffset += new Vector2(rotationOffset.x, rotationOffset.y) * draggingInventory.GetGrid().GetCellSize();
    }

    public void StoppedDragging(GridInventory fromGridInventory, PlacedObject placedObject) {
        draggingInventory = null;
        draggingPlacedObject = null;

        Cursor.visible = true;

        // Remove item from its current inventory
        fromGridInventory.RemoveItemAt(placedObject.GetGridPosition());

        GridInventory toGridInventory = null;

        // Find out which InventoryTetris is under the mouse position
        foreach (GridInventory gridInventory in gridInventoryList) {
            Vector3 screenPoint = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(gridInventory.GetItemContainer(), screenPoint, null, out Vector2 anchoredPosition);
            Vector2Int placedObjectOrigin = gridInventory.GetGridPosition(anchoredPosition);
            placedObjectOrigin = placedObjectOrigin - mouseDragGridPositionOffset;

            if (gridInventory.IsValidGridPosition(placedObjectOrigin)) {
                toGridInventory = gridInventory;
                break;
            }
        }

        // Check if it's on top of a InventoryTetris
        if (toGridInventory != null) {
            Vector3 screenPoint = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(toGridInventory.GetItemContainer(), screenPoint, null, out Vector2 anchoredPosition);
            Vector2Int placedObjectOrigin = toGridInventory.GetGridPosition(anchoredPosition);
            placedObjectOrigin = placedObjectOrigin - mouseDragGridPositionOffset;

            bool tryPlaceItem = toGridInventory.TryPlaceItem(placedObject.GetPlacedObjectTypeSO() as ItemSO, placedObjectOrigin, dir);

            if (tryPlaceItem) {
                // Item placed!
            } else {
                // Cannot drop item here!
                // Drop on original position
                fromGridInventory.TryPlaceItem(placedObject.GetPlacedObjectTypeSO() as ItemSO, placedObject.GetGridPosition(), placedObject.GetDir());
            }
        } else {
            // Not on top of any Inventory Tetris!

            // Cannot drop item here!
            // Drop on original position
            fromGridInventory.TryPlaceItem(placedObject.GetPlacedObjectTypeSO() as ItemSO, placedObject.GetGridPosition(), placedObject.GetDir());
        }
    }


}
