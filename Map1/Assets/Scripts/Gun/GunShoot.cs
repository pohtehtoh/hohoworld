using System.Collections;
using UnityEngine;
using TMPro;

public class GunShoot : MonoBehaviour, IDataPersistence
{
    [Header("Gun Data")]
    public GunData gunData;

    [Header("Objects")]
    public new Transform camera;
    public Camera cam;
    public Transform firePoint;
    public GameObject weaponRenderer;
    public GameObject scopeOverlay;
    public TextMeshProUGUI text;
    public LayerMask whatIsEnemy;
    private Shoot shootInput;

    [Header("Shooting")]
    //public float firePower = 200;
    private float fireTimer;
    private bool isShooting;
    private bool isShootingNormal;
    private bool shootNormal;
    private bool wasNotAlreadyAiming;
    private bool pulled;

    [Header("Aim")]
    public bool isAiming;
    private float defaultFOV = 60;

    [Header("Animator")]
    public Animator gunAnim;

    public bool inHand;

    private void Start()
    {
        if (!weaponRenderer || !scopeOverlay) gunData.enableScope = false;
        gunData.readyToShoot = true;
        wasNotAlreadyAiming = false;
    }

    private void Awake()
    {
        shootInput = new Shoot();
    }

    private void OnEnable()
    {
        shootInput.Enable();
    }

    private void OnDisable()
    {
        shootInput.Disable();
    }

    private void Update()
    {
        Debug.DrawRay(firePoint.position, firePoint.forward);

        //if (shootInput.ShootMain.Shoot.triggered && !gunData.reloading && gunData.readyToShoot && gunData.currentAmmo > 0 && !isShooting)
        //{
        //    Shoot();
        //}

        if (shootInput.ShootMain.Reload.triggered && gunData.currentAmmo < gunData.magSize && !gunData.reloading && inHand)
        {
            Reload();
        }

        if (shootInput.ShootMain.Aim.triggered && !gunData.reloading && inHand)
        {
            OnAim();
        }

        if ((isShooting || isShootingNormal || shootNormal) && inHand)
        {
            if (!gunData.reloading && gunData.readyToShoot && gunData.currentAmmo > 0)
            {
                gunAnim.SetBool("Shake", true);

                if (fireTimer > 0) fireTimer -= Time.deltaTime;

                else
                {
                    fireTimer = gunData.fireSpeed;
                    ContinousShoot();
                }
            }

            else
            {
                if (wasNotAlreadyAiming) OnAim();
                isShooting = false;
                isShootingNormal = false;
                shootNormal = false;
                gunAnim.SetBool("Shake", false);
                fireTimer = 0;
                wasNotAlreadyAiming = false;
            }
        }

        text.SetText(gunData.currentAmmo + " / " + gunData.magSize);
    }

    public void Shoot()
    {
        if ((cam.fieldOfView == gunData.aimingFOV || isShootingNormal || shootNormal) && inHand)
        {
            gunData.readyToShoot = false;

            //Spread
            float x = Random.Range(-gunData.spread, gunData.spread);
            float y = Random.Range(-gunData.spread, gunData.spread);

            //Calculate Direction with Spread
            Vector3 direction = camera.forward + new Vector3(x, y, 0);

            if (Physics.Raycast(camera.position, direction, out RaycastHit hitInfo, gunData.maxDistance, whatIsEnemy))
            {
                IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(gunData.damage);
                }
            }

            gunData.currentAmmo--;
            OnGunShot();

            Invoke("ResetShot", gunData.timeBetweenShooting);
        }
    }

    public void ContinousShoot()
    {
        if ((cam.fieldOfView == gunData.aimingFOV || isShootingNormal) && inHand)
        {
            //Spread
            float x = Random.Range(-gunData.spread, gunData.spread);
            float y = Random.Range(-gunData.spread, gunData.spread);

            //Calculate Direction with Spread
            Vector3 direction = camera.forward + new Vector3(x, y, 0);

            if (Physics.Raycast(camera.position, direction, out RaycastHit hitInfo, gunData.maxDistance, whatIsEnemy))
            {
                IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(gunData.damage);
                }
            }

            gunData.currentAmmo--;
            OnGunShot();
        }
    }

    private void ResetShot()
    {
        gunData.readyToShoot = true;
    }

    private void OnGunShot()
    {

    }

    public void NormalPullTrigger()
    {
        if (!gunData.reloading && gunData.readyToShoot && gunData.currentAmmo > 0 && inHand)
        {
            if (gunData.allowContinuousShooting)
            {
                if (gunData.fireSpeed > 0) isShootingNormal = true;
                else
                {
                    shootNormal = true;
                    Shoot();
                }
            }

            else
            {
                shootNormal = true;
                Shoot();
            }
        }
    }

    public void NormalReleaseTrigger()
    {
        isShooting = false;
        isShootingNormal = false;
        shootNormal = false;
        gunAnim.SetBool("Shake", false);
        fireTimer = 0;
    }

    public void AimPullTrigger()
    {
        if (!gunData.reloading && gunData.readyToShoot && gunData.currentAmmo > 0 && inHand)
        {
            pulled = true;

            if (!isAiming)
            {
                wasNotAlreadyAiming = true;
                OnAim();
            }

            if (gunData.allowContinuousShooting)
            {
                if (gunData.fireSpeed > 0) isShooting = true;
                else Shoot();
            }
        }
    }

    public void AimReleaseTrigger()
    {
        if (pulled)
        {
            if (!gunData.allowContinuousShooting && !gunData.reloading && gunData.readyToShoot && gunData.currentAmmo > 0) Shoot();
            if (wasNotAlreadyAiming) OnAim();
            isShooting = false;
            gunAnim.SetBool("Shake", false);
            fireTimer = 0;
            wasNotAlreadyAiming = false;
            pulled = false;
        }
    }

    private void Reload()
    {
        gunData.reloading = true;
        if(isAiming) OnAim();
        //reload anim
        Invoke("ReloadFinished", gunData.reloadTime);
    }

    private void ReloadFinished()
    { 
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private IEnumerator Aim()
    {
        float blendValue = 0, timeElapsed = 0;

        if (gunData.enableScope)
        {
            weaponRenderer.SetActive(true);
            scopeOverlay.SetActive(false);
        }

        while (timeElapsed < gunData.aimSpeed)
        {
            blendValue = timeElapsed / gunData.aimSpeed;

            if (isAiming)
            {
                gunAnim.SetFloat("Aiming", blendValue);
                cam.fieldOfView = Mathf.Lerp(gunData.aimingFOV, defaultFOV, 1 - blendValue);
            }

            else
            {
                gunAnim.SetFloat("Aiming", 1 - blendValue);
                cam.fieldOfView = Mathf.Lerp(gunData.aimingFOV, defaultFOV, blendValue);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (gunData.enableScope)
        {
            weaponRenderer.SetActive(!isAiming);
            scopeOverlay.SetActive(isAiming);
        }

        if (isAiming)
        {
            gunAnim.SetFloat("Aiming", 1);
            cam.fieldOfView = gunData.aimingFOV;
        }

        else
        {
            gunAnim.SetFloat("Aiming", 0);
            cam.fieldOfView = defaultFOV;
        }
    }

    public void OnAim()
    {
        StopAllCoroutines();
        isAiming = !isAiming;
        StartCoroutine(Aim());
    }

    public void LoadData(GameData data)
    {
        if(this.gunData.name == "3")
        {
            this.gunData.currentAmmo = data.gunData1Ammo;
        }
        else if(this.gunData.name == "2")
        {
            this.gunData.currentAmmo = data.gunData2Ammo;
        }
        else if(this.gunData.name == "Sniper")
        {
            this.gunData.currentAmmo = data.gunData3Ammo;
        }
    }

    public void SaveData(GameData data)
    {
        if(this.gunData.name == "3")
        {
            data.gunData1Ammo = this.gunData.currentAmmo;
        }
        else if(this.gunData.name == "2")
        {
            data.gunData2Ammo = this.gunData.currentAmmo;
        }
        else if(this.gunData.name == "Sniper")
        {
            data.gunData3Ammo = this.gunData.currentAmmo;
        }
    }
}
