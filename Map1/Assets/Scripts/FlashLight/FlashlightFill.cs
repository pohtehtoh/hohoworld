using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightFill : MonoBehaviour
{
    [SerializeField] private Image fillProgress = null;
    public Gradient gradient;

    private void Update() {
        Light flashlightLight = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Flashlight>().gameObject.GetComponent<Light>();
        float flashLightCurrentIntensity = flashlightLight.intensity;
        float flashLightMaxIntensity = flashlightLight.gameObject.GetComponent<Flashlight>().maxBrightness;
        SetBattery(flashLightCurrentIntensity, flashLightMaxIntensity);
    }
    public void SetMaxBattery(float maxBattery)
    {
        fillProgress.fillAmount = maxBattery;

        fillProgress.color = gradient.Evaluate(1f);
    }

    public void SetBattery(float currentBattery, float maxBattery)
    {
        fillProgress.fillAmount = currentBattery / maxBattery;

        fillProgress.color = gradient.Evaluate(fillProgress.fillAmount);
    }
}
