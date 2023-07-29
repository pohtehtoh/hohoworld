using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class UseAbleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName1 => "Use";

        public string ActionName2 => "Give";
        [field: SerializeField]
        public AudioClip action1SFX { get; private set; }

        public AudioClip action2SFX { get; private set; }

        public bool PerformActionOne(GameObject character, InventoryItem inventoryItem)
        {
            Flashlight flashlight = character.GetComponentInChildren<Flashlight>();
            InventoryController inventoryController = character.GetComponent<InventoryController>();
            PickUpSystem pickUpSystem = character.GetComponent<PickUpSystem>();
            if(Name == "Battery")
            {
                if(inventoryController.GetGameObject("Flashlight") != null)
                {
                    if(flashlight != null)
                    {
                        if(flashlight.gameObject.GetComponent<Light>().intensity < flashlight.maxBrightness)
                        {
                            flashlight.ReplaceBattery();
                            return true;
                        }
                        else
                        {
                            if(pickUpSystem != null)
                            {
                                pickUpSystem.AddActionText("Your battery is full", Color.red);
                            }
                            return false;
                        }
                    }
                }
                else
                {
                    if(pickUpSystem != null)
                    {
                        pickUpSystem.AddActionText("You dont have a flashlight", Color.red);
                    }
                    return false;
                }
            }
            else if(Name == "Key")
            {
                foreach(GameObject interactable in inventoryController.interactables)
                {
                    Lock keyLock = interactable.GetComponentInChildren<Lock>();
                    if(keyLock != null)
                    {
                        if(Vector3.Distance(character.transform.position, keyLock.gameObject.transform.position) <= 3)
                        {
                            keyLock.OpenLock(character);
                            return true;
                        }
                        else
                        {
                            if(pickUpSystem != null)
                            {
                                pickUpSystem.AddActionText("There is no lock nearby", Color.red);
                            }
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        public bool PerformActionTwo(GameObject character, InventoryItem inventoryItem)
        {
            throw new System.NotImplementedException();
        }
    }
}