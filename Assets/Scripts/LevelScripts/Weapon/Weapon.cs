using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Weapon : MonoBehaviour
{
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
            }
        } 
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                firstPersonController.Fire();
                Shoot();
            }
        }
        UpdateManager();
    }

    void UpdateManager()
    {
        weaponManager.currentAmmo = currentAmmo;
        weaponManager.maxAmmo = maxAmmo;
    }
    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;
        //RaycastHit hit;
        //if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        //{
        //    Debug.Log(hit.transform.name);
        //}
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(reloadTime - .25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
