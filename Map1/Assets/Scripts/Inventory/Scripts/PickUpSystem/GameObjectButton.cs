using UnityEngine;
using UnityEngine.EventSystems;

public class GameObjectButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject hitObject = pointerEventData.pointerCurrentRaycast.gameObject;
        if (hitObject.GetComponent<Item>() != null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpSystem>().PickUp(hitObject.GetComponent<Item>());
        }
    }
}
