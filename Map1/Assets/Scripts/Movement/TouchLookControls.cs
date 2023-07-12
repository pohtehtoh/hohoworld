using UnityEngine;

public class TouchLookControls : MonoBehaviour/*, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler*/
{
    public bool enableMouseMovements;
    private GameObject player;
    //public Camera cam;
    // private Wiring wiring;
    // public int index;
    public Vector2 TouchDist;
    // public bool Pressed;
    // public bool Dragged;
    // public float zoomOutMin = 1;
    // public float zoomOutMax = 0;
    // private PointerEventData data;
    // public float zoomSpeed = 0.01f;
    // private float dist;
    // private float prevDist;
    // private List<PointerEventData> m_Pointers = new();

    private int touchFingerId;
    private float halfScreenWidth;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // wiring = player.GetComponent<Wiring>();

        touchFingerId = -1;
        halfScreenWidth = Screen.width / 2;
    }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     wiring.touchFieldIndex = index;
    //     if (m_Pointers.Count > 1)
    //     {
    //         Dragged = false;
    //     }
    //     else
    //     {
    //         Dragged = true;
    //         data = eventData;
    //     }
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     Dragged = false;
    // }

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     m_Pointers.Add(eventData);
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     m_Pointers.Remove(eventData);
    // }

    // private void Zoom(float increment)
    // {
    //     Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    // }

    private void Update()
    {
        // if (m_Pointers.Count > 1)
        // {
        //     dist = Vector2.Distance(m_Pointers[0].position, m_Pointers[1].position);
        //     float difference = dist - prevDist;
        //     Zoom(difference * zoomSpeed);
        //     prevDist = dist;
        // }
        // else if (Dragged)
        //     TouchDist = data.delta;
        // else
        //     TouchDist = Vector2.zero;

        // if (Input.touchCount > 0)
        // {

        // if (enableMouseMovements)
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         if (Input.mousePosition.x > halfScreenWidth)
        //         {
        //             TouchDist = Input.delta
        //         }
        //     }
        // }
        // else
        // {
            for (int i = 0; i < Input.touchCount; i++)
            {

                Touch t = Input.GetTouch(i);

                switch (t.phase)
                {
                    case TouchPhase.Began:

                        if (t.position.x > halfScreenWidth && touchFingerId == -1)
                        {
                            touchFingerId = t.fingerId;
                            // wiring.touchFieldIndex = index;
                        }

                        break;
                    case TouchPhase.Ended:

                        if (t.fingerId == touchFingerId)
                        {
                            TouchDist = Vector2.zero;
                            touchFingerId = -1;
                        }

                        break;
                    case TouchPhase.Canceled:

                        if (t.fingerId == touchFingerId)
                        {
                            touchFingerId = -1;
                        }

                        break;
                    case TouchPhase.Moved:

                        if (t.fingerId == touchFingerId)
                        {
                            TouchDist = t.deltaPosition;
                        }

                        break;
                    case TouchPhase.Stationary:

                        if (t.fingerId == touchFingerId)
                        {
                            TouchDist = Vector2.zero;
                        }

                        break;
                    }
            
            }
        
    }
}
