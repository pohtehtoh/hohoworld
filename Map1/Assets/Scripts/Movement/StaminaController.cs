using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [SerializeField]
    // private InventorySO inventoryData;
    private FirstPersonController fps;
    // private GunShooting gun;
    [Header("Stamina Bar UI Elements")]
    [SerializeField] private Image staminaProgressUI = null;

    [Header("Stamina Values")]
    public float playerStamina = 400.0f;
    public float maxStamina = 400.0f;

    [Header("Check")]
    public bool weAreSprinting = false;
    public bool canSprint = true;

    public static StaminaController instance;

//
    [SerializeField] private float staminaUseMultiplier = 5;
    [SerializeField] private float timeBeforeStaminaRegenStarts = 3;
    [SerializeField] private float staminaValueIncrement = 3;
    [SerializeField] private float staminaTimeIncrement = 0.1f;
    private Coroutine regeneratingStamina;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        fps = GetComponent<FirstPersonController>();
    }
    private void Update()
    {
        // if (PauseMenu.paused)
        //     StopAllCoroutines();

        CheckSprint();
    }

    private void CheckSprint()
    {
        if (playerStamina > 0/* && inventoryData.Weight < inventoryData.maxWeight*/)
        {
            // gun = gameObject.GetComponentInChildren<GunShooting>();
            // if (gun != null && gun.isAiming) canSprint = false;
            /*else */canSprint = true;
        }
        else canSprint = false;
    }

    private IEnumerator StaminaRegen()
    {
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);

        while(playerStamina < maxStamina)
        {
            while(!(fps.moveAxis.y > 0.9))
            {
                yield return new WaitForSeconds(timeBeforeStaminaRegenStarts);
                while(playerStamina < maxStamina)
                {
                    playerStamina += staminaValueIncrement;
                    UpdateStamina();

                    if (playerStamina > maxStamina) playerStamina = maxStamina;

                    yield return timeToWait;
                }
            }
            yield return new WaitForSeconds(1);
        }

        regeneratingStamina = null;
    }
    public void UseStamina()
    {
        if(regeneratingStamina != null)
        {
            StopCoroutine(regeneratingStamina);
            regeneratingStamina = null;
        }

        playerStamina -= staminaUseMultiplier * Time.deltaTime;
        UpdateStamina();

        if(playerStamina < 0) playerStamina = 0;

        if(playerStamina <= 0) canSprint = false;

        if (playerStamina < maxStamina && regeneratingStamina == null)
        {
            regeneratingStamina = StartCoroutine(StaminaRegen());
        }
    }

    void UpdateStamina()
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;
    }
}
