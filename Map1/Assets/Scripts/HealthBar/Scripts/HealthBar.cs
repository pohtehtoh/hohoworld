using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillProgress = null;
    public Gradient gradient;

    public void SetMaxHealth(float maxHealth)
    {
        fillProgress.fillAmount = maxHealth;

        fillProgress.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        fillProgress.fillAmount = currentHealth / maxHealth;

        fillProgress.color = gradient.Evaluate(fillProgress.fillAmount);
    }
}
