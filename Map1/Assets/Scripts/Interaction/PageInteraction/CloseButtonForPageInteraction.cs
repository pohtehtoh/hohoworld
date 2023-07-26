using UnityEngine;
using UnityEngine.EventSystems;

public class CloseButtonForPageInteraction : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PageInteraction[] pageInteractions = GameObject.FindObjectsOfType<PageInteraction>();
        foreach (PageInteraction page in pageInteractions)
        {
            if (page.isClicked)
            {
                page.isClicked = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
