using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip doorOpeningClip;
    [SerializeField] private AudioClip doorClosingClip;
    private AudioSource doorAudioSource;
    private Animator doorAnimator;
    private bool isOpen = false;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        doorAudioSource = GetComponent<AudioSource>();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject hitObject = pointerEventData.pointerCurrentRaycast.gameObject;
        if (hitObject != null)
        {
            if (Vector3.Distance(hitObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 5)
            {
                if (!isOpen)
                {
                    isOpen = true;
                    doorAudioSource.PlayOneShot(doorOpeningClip);
                    doorAnimator.SetTrigger("OpenDoor");
                }
                else
                {
                    isOpen = false;
                    doorAudioSource.PlayOneShot(doorClosingClip);
                    doorAnimator.SetTrigger("CloseDoor");
                }
            }
        }
    }
}
