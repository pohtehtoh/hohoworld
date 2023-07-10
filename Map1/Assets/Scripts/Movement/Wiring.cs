using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiring : MonoBehaviour
{
    private TouchLookControls touchField;
    // public int touchFieldIndex;

    private Player playerInput;

    private void Awake()
    {
        playerInput = new Player();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        touchField = GetComponent<TouchLookControls>();
    }

    void Update()
    {
        var fps = GetComponent<FirstPersonController>();

        // if(playerInput.PlayerMain.Save.triggered)
        // {
        //     DataPersistenceManager.instance.SaveGame();
        // }

        fps.moveAxis = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        fps.jumpAxis = playerInput.PlayerMain.Jump.triggered;
        fps.m_MouseLook.lookAxis = touchField.TouchDist;

        if (playerInput.PlayerMain.Crouch.triggered)
        {
            fps.crouchAxis = !fps.crouchAxis;
        }
    }
}
