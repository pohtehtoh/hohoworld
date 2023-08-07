using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipeInformation : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_Text description;
    [SerializeField]
    private TMP_Text required;

    public void Awake()
    {
        Hide();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void SetDescription(Sprite itemImage, string itemName, string itemDescription, string itemsRequired)
    {
        Show();
        image.sprite = itemImage;
        title.text = itemName;
        description.text = itemDescription;
        required.text = itemsRequired;
    }
}
