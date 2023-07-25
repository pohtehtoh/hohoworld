using Inventory.Model;
using Inventory.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridInventoryDragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private UIInventoryPage inventoryPage;
    private GridInventory gridInventory;
    private PlacedObject placedObject;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        placedObject = GetComponent<PlacedObject>();
        inventoryPage = this.gameObject.GetComponentInParent<UIInventoryPage>();
    }

    public void Setup(GridInventory gridInventory)
    {
        this.gridInventory = gridInventory;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;

        ItemSO.CreateVisualGrid(transform.GetChild(0), placedObject.GetPlacedObjectTypeSO() as ItemSO, gridInventory.GetGrid().GetCellSize());
        GridInventoryDragDropSystem.Instance.StartedDragging(gridInventory, placedObject);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        GridInventoryDragDropSystem.Instance.StoppedDragging(gridInventory, placedObject);
        inventoryPage.ResetSelection();
    }
}
