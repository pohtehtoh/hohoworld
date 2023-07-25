using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;
using UnityEngine.UI;

public class InHandWeapon : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private WeaponSwitch weaponSwitch;
    [SerializeField]
    private InventoryController inventoryController;
    [SerializeField]
    private GameObject gunSlot1;
    [SerializeField]
    private GameObject gunSlot2;
    [SerializeField]
    private ItemSO leftWeapon = null;
    [SerializeField]
    private ItemSO rightWeapon = null;
    [SerializeField]
    private ItemSO inHandWeapon = null;
    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;
    private InventoryAction inventoryInput;
    private bool inHandWeaponIsFree = true;

    private void Awake()
    {
        inventoryInput = new InventoryAction();

        weaponSwitch = GetComponentInChildren<WeaponSwitch>();
        inventoryController = GetComponent<InventoryController>();
    }
    private void OnEnable()
    {
        inventoryInput.Enable();
    }
    private void OnDisable()
    {
        inventoryInput.Disable();
    }
    private void Update()
    {
        if (inventoryInput.InventoryMain.GunSlot1.triggered)
        {
            if (leftWeapon != null) EquipWeapon(leftWeapon, itemCurrentState);
        }
        if (inventoryInput.InventoryMain.GunSlot2.triggered)
        {
            if (rightWeapon != null) EquipWeapon(rightWeapon, itemCurrentState);
        }
    }

    private void SetUp()
    {
        if (leftWeapon != null)
        {
            gunSlot1.GetComponent<Image>().enabled = true;
            gunSlot1.GetComponent<Image>().sprite = leftWeapon.ItemImage;
        }
        if (rightWeapon != null)
        {
            gunSlot2.GetComponent<Image>().enabled = true;
            gunSlot2.GetComponent<Image>().sprite = rightWeapon.ItemImage;
        }
        if (inHandWeapon != null)
        {
            EquipWeapon(this.inHandWeapon, itemCurrentState);
        }
    }

    public void RemoveWeapon(ItemSO inventoryItem)
    {
        if (inHandWeapon == inventoryItem)
        {
            weaponSwitch.RemoveWeapon(inHandWeapon);
            inHandWeapon = null;
            inHandWeaponIsFree = true;
        }

        if (leftWeapon == inventoryItem)
        {
            gunSlot1.GetComponent<Image>().sprite = null;
            gunSlot1.GetComponent<Image>().enabled = false;
            leftWeapon = null;
        }
        else if(rightWeapon == inventoryItem)
        {
            gunSlot2.GetComponent<Image>().sprite = null;
            gunSlot2.GetComponent<Image>().enabled = false;
            rightWeapon = null;
        }

        if (leftWeapon == null && rightWeapon == null)
        {
            weaponSwitch.shootingbuttons.SetActive(false);
        }
    }
    public void SetWeapon(ItemSO inventoryItem, List<ItemParameter> itemState)
    {
        if(leftWeapon == null)
        {
            leftWeapon = inventoryItem;
        }
        else if(rightWeapon == null)
        {
            rightWeapon = inventoryItem;
        }
        else
        {
            if (inventoryController.GetGameObject(inHandWeapon.Name) != null)
            {
                inventoryController.GetGameObject(inHandWeapon.Name).GetComponent<Item>().useOtherAction = false;
            }
            if(inHandWeapon == leftWeapon)
            {
                leftWeapon = inventoryItem;
            }
            else  
            {
                rightWeapon = inventoryItem;
            }
        }
        EquipWeapon(inventoryItem, itemState);
    }
    private void EquipWeapon(ItemSO inventoryItem, List<ItemParameter> itemState)
    {
        inHandWeaponIsFree = false;
        if(inventoryItem == inHandWeapon) return;
        if (inHandWeapon != null) weaponSwitch.RemoveWeapon(inHandWeapon);
        this.inHandWeapon = inventoryItem;
        weaponSwitch.EquipWeapon(inventoryItem);
        this.itemCurrentState = new List<ItemParameter>(itemState == null ? inventoryItem.DefaultParametersList : itemState);
        SetUp();
        // ModifyParameters();
    }
    private void SwapWeapon(EquiptableItemSO weaponItemSO1, EquiptableItemSO weaponItemSO2)
    {
        EquiptableItemSO weaponNew = weaponItemSO1;
        weaponItemSO1 = weaponItemSO2;
        weaponItemSO2 = weaponNew;
    }
    private void ModifyParameters()
    {
        foreach(var parameter in parametersToModify)
        {
            if(itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }

    public void Swap()
    {
        // SwapWeapon(leftWeapon, rightWeapon);
        // gunSlot1.GetComponent<Image>().sprite = leftWeapon.ItemImage;
        // gunSlot2.GetComponent<Image>().sprite = rightWeapon.ItemImage;
    }
    public void LoadData(GameData data)
    {
        // this.leftWeapon = inventoryController.GetGameObject(data.leftWeapon).GetComponent<Item>().InventoryItem;
        // this.rightWeapon = inventoryController.GetGameObject(data.rightWeapon).GetComponent<Item>().InventoryItem;
        // this.inHandWeapon = inventoryController.GetGameObject(data.inHandWeapon).GetComponent<Item>().InventoryItem;
        this.leftWeapon = data.leftWeapon;
        this.rightWeapon = data.rightWeapon;
        this.inHandWeapon = data.inHandWeapon;
        SetUp();
    }
    public void SaveData(GameData data)
    {
        data.leftWeapon = this.leftWeapon;
        data.rightWeapon = this.rightWeapon;
        data.inHandWeapon = this.inHandWeapon;
    }
}
