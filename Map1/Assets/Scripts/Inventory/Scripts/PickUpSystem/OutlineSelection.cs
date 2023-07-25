using UnityEngine;

public class OutlineSelection : MonoBehaviour
{
    private GameObject[] selectables;

    void Update()
    {
        selectables = GameObject.FindGameObjectsWithTag("Selectable");
        foreach(GameObject highlight in selectables)
        {
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, highlight.transform.position) < 3)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
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