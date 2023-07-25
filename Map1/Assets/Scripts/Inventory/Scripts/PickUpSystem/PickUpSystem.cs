using Inventory.Model;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    [Min(1)]
    private float hitRange = 3;
    private InventoryAction inventoryInput;

    private void Awake()
    {
        inventoryInput = new InventoryAction();
    }
    private void OnEnable()
    {
        inventoryInput.Enable();
    }
    private void OnDisable()
    {
        inventoryInput.Disable();
    }
    private bool CanPickUp(Item item)
    {
        if (item.itemType == Item.ItemType.weapon)
        {
            WeaponSwitch weaponSwitch = GetComponentInChildren<WeaponSwitch>();
            foreach (Transform weapon in weaponSwitch.transform)
            {
                if (item.InventoryItem.Name == weapon.gameObject.GetComponent<Item>().InventoryItem.Name)
                {
                    return false;
                }
            }
            return true;
        }
        return true;
    }
    public void PickUp(Item item)
    {
        if (Vector3.Distance(item.gameObject.transform.position, this.gameObject.transform.position) <= hitRange)
        {
            if (CanPickUp(item))
            {
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if(reminder == 0)
                {
                    item.PickUpItem();
                }
                else
                    item.Quantity = reminder;
            }
        }
    }
}
