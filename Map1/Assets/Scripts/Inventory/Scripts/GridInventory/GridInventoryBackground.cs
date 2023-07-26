using Inventory.UI;
using UnityEngine;
using UnityEngine.UI;

public class GridInventoryBackground : MonoBehaviour {

    [SerializeField] private GridInventory gridInventory;
    [SerializeField] private UIInventoryPage inventoryPage;

    private void Start() {
        // Create background
        Transform template = transform.Find("Template");
        template.gameObject.SetActive(false);

        for (int x = 0; x < gridInventory.GetGrid().GetWidth(); x++) {
            for (int y = 0; y < gridInventory.GetGrid().GetHeight(); y++) {
                Transform backgroundSingleTransform = Instantiate(template, transform);
                backgroundSingleTransform.gameObject.SetActive(true);
            }
        }

        GetComponent<GridLayoutGroup>().cellSize = new Vector2(gridInventory.GetGrid().GetCellSize(), gridInventory.GetGrid().GetCellSize());

        GetComponent<RectTransform>().sizeDelta = new Vector2(gridInventory.GetGrid().GetWidth(), gridInventory.GetGrid().GetHeight()) * gridInventory.GetGrid().GetCellSize();

        //GetComponent<RectTransform>().anchoredPosition = gridInventory.GetComponent<RectTransform>().anchoredPosition;
    }

}