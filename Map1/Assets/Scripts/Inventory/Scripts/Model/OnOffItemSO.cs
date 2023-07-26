using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class OnOffItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName1 => "On";

        public string ActionName2 => "Off";
        [field: SerializeField]
        public AudioClip action1SFX { get; private set; }

        public AudioClip action2SFX { get; private set; }

        public bool PerformActionOne(GameObject character, InventoryItem inventoryItem)
        {
            Flashlight flashlight = character.GetComponentInChildren<Flashlight>();
            InventoryController inventoryController = character.GetComponent<InventoryController>();
            PickUpSystem pickUpSystem = character.GetComponent<PickUpSystem>();
            if(flashlight != null)
            {
                flashlight.gameObject.GetComponent<Light>().enabled = true;
                if(flashlight.gameObject.GetComponent<Light>().intensity <= 0)
                {
                    if(pickUpSystem != null)
                    {
                        pickUpSystem.AddActionText("Your battery is dead. Recharge your battery", Color.red);
                    }
                    flashlight.gameObject.GetComponent<Light>().enabled = false;
                    return false;
                }
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
            Flashlight flashlight = character.GetComponentInChildren<Flashlight>();
            InventoryController inventoryController = character.GetComponent<InventoryController>();
            if(flashlight != null)
            {
                flashlight.gameObject.GetComponent<Light>().enabled = false;
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
