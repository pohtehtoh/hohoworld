using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquiptableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName1 => "Equip";
        public string ActionName2 => "Unequip";
        [field: SerializeField]
        public int Index { get; set; }
        [field: SerializeField]
        public AudioClip action1SFX { get; private set; }

        public AudioClip action2SFX { get; private set; }

        public bool PerformActionOne(GameObject character, InventoryItem inventoryItem)
        {
            InHandWeapon weaponSystem = character.GetComponent<InHandWeapon>();
            InventoryController inventoryController = character.GetComponent<InventoryController>();
            if(weaponSystem != null)
            {
                weaponSystem.SetWeapon(inventoryItem.item, inventoryItem.itemState);
                if (inventoryController != null)
                {
                    if (inventoryController.GetGameObject(inventoryItem.item.Name) != null)
                    {
                        inventoryController.GetGameObject(inventoryItem.item.Name).GetComponent<Item>().useOtherAction = true;
                    }
                }
                return true;
            }
            return false;
        }

        public bool PerformActionTwo(GameObject character, InventoryItem inventoryItem)
        {
            InHandWeapon weaponSystem = character.GetComponent<InHandWeapon>();
            InventoryController inventoryController = character.GetComponent<InventoryController>();
            if(weaponSystem != null)
            {
                weaponSystem.RemoveWeapon(inventoryItem.item);
                if (inventoryController != null)
                {
                    if (inventoryController.GetGameObject(inventoryItem.item.Name) != null)
                    {
                        inventoryController.GetGameObject(inventoryItem.item.Name).GetComponent<Item>().useOtherAction = false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}