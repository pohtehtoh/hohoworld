using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();
        public string ActionName1 => "Consume";
        public string ActionName2 => "Give";
        [field: SerializeField]
        public AudioClip action1SFX { get; private set; }

        public AudioClip action2SFX { get; private set; }

        public bool PerformActionOne(GameObject character, InventoryItem inventoryItem)
        {
            foreach(ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        }
        
        public bool PerformActionTwo(GameObject character, InventoryItem inventoryItem)
        {
            throw new NotImplementedException();
        }
    }
    public interface IDestroyableItem
    {

    }
    public interface IItemAction
    {
        public string ActionName1 { get; }
        public string ActionName2 { get; }
        public AudioClip action1SFX { get; }
        public AudioClip action2SFX { get; }
        bool PerformActionOne(GameObject character, InventoryItem inventoryItem);
        bool PerformActionTwo(GameObject character, InventoryItem inventoryItem);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}