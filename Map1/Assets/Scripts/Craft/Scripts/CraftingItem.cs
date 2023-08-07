using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CraftingUI craftingUI;
    [SerializeField] private CraftingRecipe craftingRecipe;
    [SerializeField] private Image itemImage;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        craftingUI.WriteRecipeInformation(craftingRecipe, itemImage.sprite);
    }
}
