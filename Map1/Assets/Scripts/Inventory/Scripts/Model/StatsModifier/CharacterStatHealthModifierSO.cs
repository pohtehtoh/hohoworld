using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        FirstPersonController fps = character.GetComponent<FirstPersonController>();
        fps.currentHealth += val;
        if (fps.currentHealth > fps.maxHealth)
        {
            fps.currentHealth = fps.maxHealth;
        }
        //fps.healthBar.SetHealth(fps.currentHealth, fps.maxHealth);
    }
}
