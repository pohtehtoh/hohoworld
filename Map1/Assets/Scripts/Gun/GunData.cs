using UnityEngine;

[CreateAssetMenu(fileName="Gun", menuName="Weapon/Gun")]
public class GunData : ScriptableObject
{ 
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;
    public float timeBetweenShooting;
    public float fireSpeed;
    [HideInInspector]
    public bool readyToShoot;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;

    [Header("Aiming")]
    public float aimSpeed;
    public float aimingFOV;

    [Header("Accuracy")]
    public float spread;

    [Header("Check")]
    public bool allowContinuousShooting;
    public bool enableScope;
}