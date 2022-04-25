using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Weapon : MonoBehaviour
{
    public enum levelEnum
    {
        BASIC,
        ANTICIPATION,
        MOVING
    }

    [SerializeField]
    private levelEnum levelType = levelEnum.BASIC;
    public GameControl gameControlBasic;
    public GameControl_Moving gameControlMoving;
    public GameControl_Anticipation gameControlAnticipation;

    public bool automatic = true;
    public float fireRate = 10f;
    public ParticleSystem muzzleFlash;

    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator animator;

    private float nextTimeToFire = 0f;

    public WeaponManager weaponManager;
    public FirstPersonController firstPersonController;

    private void Start()
    {
        currentAmmo = maxAmmo;
        if (!automatic)
        {
            fireRate = 7f;
        }
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (automatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                firstPersonController.Fire();
                Shoot();
                ShootGameControlRay();
            }
        } 
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                firstPersonController.Fire();
                Shoot();
                ShootGameControlRay();
            }
        }
        UpdateManager();
    }

    void ShootGameControlRay()
    {
        if (levelType == levelEnum.BASIC)
        {
            gameControlBasic.ShootRay();
        } 
        else if (levelType == levelEnum.MOVING)
        {
            gameControlMoving.ShootRay();
        }
        else if (levelType == levelEnum.ANTICIPATION)
        {
            gameControlAnticipation.ShootRay();
        }
    }

    void UpdateManager()
    {
        weaponManager.currentAmmo = currentAmmo;
        weaponManager.maxAmmo = maxAmmo;
        weaponManager.automatic = automatic;
        if (Time.time >= nextTimeToFire)
        {
            weaponManager.canFire = true;
        } else
        {
            weaponManager.canFire = false;
        }
    }
    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(reloadTime - .25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
