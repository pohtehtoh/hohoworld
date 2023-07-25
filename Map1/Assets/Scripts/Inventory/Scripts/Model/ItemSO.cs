using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Model
{
    public abstract class ItemSO : PlacedObjectTypeSO
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();
        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;
        [field: SerializeField]
        public string Name { get; set; }
        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }
        [field: SerializeField]
        public Sprite ItemImage { get; set; }
        [field: SerializeField]
        public int ItemWeight { get; set; }
        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }

        public static void CreateVisualGrid(Transform visualParentTransform, ItemSO itemSO, float cellSize)
        {
            Transform visualTransform = Instantiate(GridInventoryAssets.Instance.gridVisual, visualParentTransform);

            // Create background
            Transform template = visualTransform.Find("Template");
            template.gameObject.SetActive(false);

            for (int x = 0; x < itemSO.width; x++)
            {
                for (int y = 0; y < itemSO.height; y++)
                {
                    Transform backgroundSingleTransform = Instantiate(template, visualTransform);
                    backgroundSingleTransform.gameObject.SetActive(true);
                }
            }

            visualTransform.GetComponent<GridLayoutGroup>().cellSize = Vector2.one * cellSize;

            visualTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(itemSO.width, itemSO.height) * cellSize;

            visualTransform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            visualTransform.SetAsFirstSibling();
        }
    }
    
    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;
        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}