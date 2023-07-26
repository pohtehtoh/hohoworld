using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageInteraction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float rotateInHandSpeed = 1000f;
    [SerializeField] private Transform readTransform;
    [SerializeField] private Button closeButton;
    private Transform playerTransform;
    private Animator animator;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public bool isClicked = false;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (Vector3.Distance(readTransform.position, initialPosition) > 2)
        {
            isClicked = false;
        }

        PageInteraction[] pageInteractions = GameObject.FindObjectsOfType<PageInteraction>();
        foreach (PageInteraction page in pageInteractions)
        {
            if (page.isClicked && Vector3.Distance(readTransform.position, page.initialPosition) > 2)
            {
                closeButton.gameObject.SetActive(false);
            }
        }
        
        if (isClicked)
        {
            MoveTowardsPlayer();
        }

        else
        {
            MoveBackToPosition();
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject hitObject = pointerEventData.pointerCurrentRaycast.gameObject;
        if (Vector3.Distance(readTransform.position, this.gameObject.transform.position) <= 2 && hitObject.GetComponent<PageInteraction>() != null)
        {
            PageInteraction[] pageInteractions = GameObject.FindObjectsOfType<PageInteraction>();
            foreach (PageInteraction page in pageInteractions)
            {
                if (page.isClicked) page.isClicked = false;
            }
            isClicked = true;
            closeButton.gameObject.SetActive(true);
        }
    }
    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, readTransform.position, moveSpeed * Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
        // transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateInHandSpeed * Time.deltaTime);
    }
    private void MoveBackToPosition()
    {
        transform.position = Vector3.Lerp(transform.position, initialPosition, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, rotateSpeed * Time.deltaTime);
    }
}
