using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    // private Transform highlight;
    private GameObject[] selectables;

    void Update()
    {
        selectables = GameObject.FindGameObjectsWithTag("Selectable");
        foreach(GameObject highlight in selectables)
        {
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, highlight.transform.position) < 10)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    // highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    // highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = false;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = false;
                }
            }
        }
    }

}