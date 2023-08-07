using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingTable : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject hitObject = pointerEventData.pointerCurrentRaycast.gameObject;
        if (hitObject != null)
        {
            if (Vector3.Distance(hitObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 5)
            {
                CraftingController craftingController = GameObject.FindGameObjectWithTag("Player").GetComponent<CraftingController>();
                if(craftingController != null)
                {
                    craftingController.craftingUI.Show();
                }
            }
        }
    }
}
