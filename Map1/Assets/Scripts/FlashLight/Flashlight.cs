using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashLight;
    [SerializeField] private float maxBrightness;
    [SerializeField] private float minBrightness;
    [SerializeField] private float drainRate;

    private void Start()
    {
        flashLight = GetComponent<Light>();
    }
    private void Update()
    {
        flashLight.intensity = Mathf.Clamp(flashLight.intensity, minBrightness, maxBrightness);
        if(flashLight.enabled)
        {
            if(flashLight.intensity > minBrightness)
            {
                flashLight.intensity -= Time.deltaTime * drainRate;
            }
        }
    }
    public void ReplaceBattery()
    {
        if(flashLight.intensity >= maxBrightness)
        {
            flashLight.intensity = maxBrightness;
            Debug.Log("Flashlight battery full");
        }
        else
        {
            flashLight.intensity += 10f;
            if(flashLight.intensity > maxBrightness) flashLight.intensity = maxBrightness;
        }
    }
}
