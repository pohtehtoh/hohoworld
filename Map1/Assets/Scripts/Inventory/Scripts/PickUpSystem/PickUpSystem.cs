using Inventory.Model;
using TMPro;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private GameObject actionTextPrefab;
    [SerializeField]
    private Transform actionText;
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
                if(reminder == item.Quantity)
                {
                    AddActionText("Bag full", Color.red);
                }
                else if(reminder == 0)
                {
                    item.PickUpItem();
                    AddActionText(item.InventoryItem.Name + " picked up", Color.white);
                }
                else
                    item.Quantity = reminder;
            }
        }
    }
    public void AddActionText(string text, Color textColour)
    {
        GameObject actionTextPop = Instantiate(actionTextPrefab, actionText);
        actionTextPop.GetComponent<TextMeshProUGUI>().text = text;
        actionTextPop.GetComponent<TextMeshProUGUI>().color = textColour;
    }
}