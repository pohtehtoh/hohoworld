using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class PlacedObject : MonoBehaviour {


    public InventoryItem inventoryItem;
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO) {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
        placedObject.placedObjectTypeSO = placedObjectTypeSO;
        placedObject.origin = origin;
        placedObject.dir = dir;

        placedObject.Setup();

        return placedObject;
    }

    public static PlacedObject CreateCanvas(Transform parent, Vector2 anchoredPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO) {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, parent);
        placedObjectTransform.rotation = Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
        placedObjectTransform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
        placedObject.placedObjectTypeSO = placedObjectTypeSO;
        placedObject.origin = origin;
        placedObject.dir = dir;

        placedObject.Setup();

        return placedObject;
    }



    private PlacedObjectTypeSO placedObjectTypeSO;
    private Vector2Int origin;
    private PlacedObjectTypeSO.Dir dir;

    protected virtual void Setup() {
        //Debug.Log("PlacedObject.Setup() " + transform);
    }

    public virtual void GridSetupDone() {
        //Debug.Log("PlacedObject.GridSetupDone() " + transform);
    }

    // protected virtual void TriggerGridObjectChanged() {
    //     foreach (Vector2Int gridPosition in GetGridPositionList()) {
    //         GridBuildingSystem3D.Instance.GetGridObject(gridPosition).TriggerGridObjectChanged();
    //     }
    // }

    public Vector2Int GetGridPosition() {
        return origin;
    }

    public void SetOrigin(Vector2Int origin) {
        this.origin = origin;
    }

    public List<Vector2Int> GetGridPositionList() {
        return placedObjectTypeSO.GetGridPositionList(origin, dir);
    }

    public PlacedObjectTypeSO.Dir GetDir() {
        return dir;
    }

    public virtual void DestroySelf() {
        Destroy(gameObject);
    }

    public override string ToString() {
        return placedObjectTypeSO.nameString;
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO() {
        return placedObjectTypeSO;
    }
    public InventoryItem GetInventoryItem() {
        return inventoryItem;
    }
    public InventoryItem ChangeInventoryItem(InventoryItem newInventoryItem)
    {
        this.inventoryItem = newInventoryItem;
        return inventoryItem;
    }


    [System.Serializable]
    public class SaveObject {

        public string placedObjectTypeSOName;
        public Vector2Int origin;
        public PlacedObjectTypeSO.Dir dir;
        public string floorPlacedObjectSave;

    }

}

