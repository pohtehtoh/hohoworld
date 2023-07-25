using Inventory;
using Inventory.Model;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField]
    private InventoryController inventoryController;
    public float timeBetweenSwitch;
    private bool canSwitch;
    private Shoot shootInput;
    public GameObject shootingbuttons;
    private ItemSO tempInventoryItem;

    private void Awake()
    {
        shootInput = new Shoot();
    }
    private void Start()
    {
        canSwitch = true;
        tempInventoryItem = null;

        inventoryController = GetComponentInParent<InventoryController>();
    }
    private void OnEnable()
    {
        shootInput.Enable();
    }
    private void OnDisable()
    {
        shootInput.Disable();
    }
    public void RemoveWeapon(ItemSO inventoryItem)
    {
        if (inventoryController != null)
        {
            if (inventoryController.GetGameObject(inventoryItem.Name) != null)
            {
                inventoryController.GetGameObject(inventoryItem.Name).SetActive(false);
                inventoryController.GetGameObject(inventoryItem.Name).GetComponentInChildren<GunShoot>().inHand = false;
            }
        }
    }
    public void EquipWeapon(ItemSO inventoryItem)
    {
        tempInventoryItem = inventoryItem;
        shootingbuttons.SetActive(true);
        if(canSwitch)
        {
            canSwitch = false;
            Invoke("Switch", timeBetweenSwitch);
        }
    }
    private void Switch()
    {
        if (inventoryController != null)
        {
            if (inventoryController.GetGameObject(tempInventoryItem.Name) != null)
            {
                inventoryController.GetGameObject(tempInventoryItem.Name).SetActive(true);
                inventoryController.GetGameObject(tempInventoryItem.Name).GetComponentInChildren<GunShoot>().inHand = true;
            }
        }

        tempInventoryItem = null;
        canSwitch = true;
    }
}
