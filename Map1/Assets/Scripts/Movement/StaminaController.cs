using System;
using System.Collections;
//using Inventory.Model;
using UnityEngine;
using UnityEngine.UI;
//using UnityStandardAssets.Characters.FirstPerson;

public class StaminaController : MonoBehaviour
{
    //[SerializeField]
    //private InventorySO inventoryData;
    private FirstPersonController fps;
    //private GunShooting gun;
    [Header("Stamina Bar UI Elements")]
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup SliderCanvasGroup = null;

    [Header("Stamina Values")]
    public float playerStamina = 400.0f;
    public float maxStamina = 400.0f;

    [Header("Stamina Regeneration Values")]
    [Range(0, 100)] public float staminaDrain = 1.0f;
    [Range(0, 100)] public float staminaRegen = 1.0f;

    [Header("Check")]
    public bool weAreSprinting = false;
    public bool canSprint = true;
    private bool canStartRegen = true;

    [Header("Enumerators")]
    private WaitForSeconds regenTick = new WaitForSeconds(Time.deltaTime);
    private Coroutine regen;

    public static StaminaController instance;


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
        //if (PauseMenu.paused)
        //    StopAllCoroutines();

        CheckSprint();
    }

    private void CheckSprint()
    {
        if (playerStamina >= staminaDrain/* && inventoryData.Weight < inventoryData.maxWeight*/)
        {
            //gun = gameObject.GetComponentInChildren<GunShooting>();
            //if (gun != null && gun.isAiming) 
                //canSprint = false;
            /*else*/ canSprint = true;
        }
        else canSprint = false;
    }

    IEnumerator StaminaRegen()
    {
        //cooldown time after using stamina
        yield return new WaitForSeconds(2);

        //regenerate stamina when not sprinting
        while (playerStamina < maxStamina && !weAreSprinting)
        {
            while (fps.moveAxis.y < 9f)
            {
               playerStamina += staminaRegen;
                UpdateStamina(1);
                yield return regenTick;

                if (playerStamina >= maxStamina)
                {
                    yield return new WaitForSeconds(1);
                    SliderCanvasGroup.alpha = 0;
                    canStartRegen = true;
                } 
            }
            yield return null;
        }
    }
    IEnumerator StaminaRegenWait()
    {
        while(canStartRegen)
        {
            if(fps.moveAxis.y < 9f)
            {
                canStartRegen = false;
                StartCoroutine(StaminaRegen());
            }
            yield return null;
        }
    }
    IEnumerator StaminaRegenInside()
    {
        //regenerate stamina too but after pause and without the cooldown period
        while (playerStamina < maxStamina && !weAreSprinting)
        {

            playerStamina += staminaRegen;
            UpdateStamina(1);
            yield return regenTick;

            if (playerStamina >= maxStamina)
            {
                yield return new WaitForSeconds(1);
                SliderCanvasGroup.alpha = 0;
            }
        }
    }

    public void UseStamina()
    {
        if (playerStamina >= staminaDrain)
        {
            playerStamina -= staminaDrain;
            UpdateStamina(1);
        }

        if (regen != null)
        {
            canStartRegen = true;
            StopCoroutine(regen);
        }

        regen = StartCoroutine(StaminaRegenWait());
    }

    void UpdateStamina(int value)
    {
        //stamina bar
        staminaProgressUI.fillAmount = playerStamina / maxStamina;

        if (value == 0)
        {
            SliderCanvasGroup.alpha = 0;
        }

        else
        {
            SliderCanvasGroup.alpha = 1;
        }
    }

    public void WhenResumed()
    {
        StartCoroutine(StaminaRegenInside());
    }
}
