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
            // Lock keyLock = character.GetComponent<InventoryController>().keyLock;
            // if(keyLock != null)
            // {
            //     keyLock.OpenLock(character);
            //     return true;
            // }
            return false;
        }

        public bool PerformActionTwo(GameObject character, InventoryItem inventoryItem)
        {
            throw new System.NotImplementedException();
        }
    }
}
