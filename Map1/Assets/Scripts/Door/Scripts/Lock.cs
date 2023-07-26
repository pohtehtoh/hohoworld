using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject leftGate;
    [SerializeField] private GameObject rightGate;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void OpenLock(GameObject character)
    {
        if (Vector3.Distance(character.transform.position, this.gameObject.transform.position) <= 3)
        {
            animator.Play("witchGateLock", -1, 0f);
            Invoke("OpenDoor", 2f);
        }
    }

    public void OpenDoor()
    {
        leftGate.GetComponent<Animator>().Play("witchLeftGateOpen");
        rightGate.GetComponent<Animator>().Play("witchRightGateOpen");
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.lockPosition;
        this.transform.localEulerAngles = data.lockRotation;
        // this.rightGate.transform.localEulerAngles = data.rightGateRotation;
        // this.leftGate.transform.localEulerAngles = data.leftGateRotation;
    }

    public void SaveData(GameData data)
    {
        data.lockPosition = this.transform.position;
        data.lockRotation = this.transform.localEulerAngles;
        // data.rightGateRotation = this.rightGate.transform.localEulerAngles;
        // data.leftGateRotation = this.leftGate.transform.localEulerAngles;
    }
}
