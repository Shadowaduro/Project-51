using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Guns

    [Header("----------------------------------------------------------------")]
    [Header("Guns")]
    [Header("----------------------------------------------------------------")]

    public float gunRange = 100f;
    public float gunDamage = 10f;
    public float maxAmmo = 300f;
    public float magSize = 50f;
    public float currentAmmo;
    [Range(0, 100)]
    public float reloadPercentage = 30f;

    [Space(5)]
    public float bulletDelaySeconds = 0.1f;
    public float chargeUp = 0.1f;
    public float chargeUpCap = 0.1f;
    float bulletDelayTimer;
    float maxBulletDelaySeconds;
    bool delayOver;
    bool shotFired;

    [Space(5)]
    //public float reloadDelaySeconds = 1f;
    //float reloadDelayTimer;

    [Space(5)]
    //[HideInInspector]
<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/Gun.cs
    bool reloadDone = true;
    bool reloadNeeded;
=======
    public bool reloadDone = true;
    bool reloading;
>>>>>>> Stashed changes:Project 51/Assets/Scripts/Gun.cs
    bool reloadTimer;
    bool outOfAmmo;

    [Space(5)]
    public GameObject muzzleFlash;
    public GameObject impactEffect;

    [Space(5)]
    public Transform spawnMuzzleFlash;

    [Space(5)]
    public Text reloadWarning;

    #endregion

    #region Other

    [Header("----------------------------------------------------------------")]
    [Header("Misc")]
    [Header("----------------------------------------------------------------")]

    public float raycastOffSetY = 0f;

    [Space(5)]
    public new GameObject camera;

    PlayerUI playerUI;
    PlayerAnimator playerAnimator;

    #endregion

    void Start()
    {
        playerUI = GameObject.Find("UI").GetComponent<PlayerUI>();
        playerAnimator = GetComponentInParent<PlayerAnimator>();
        maxBulletDelaySeconds = bulletDelaySeconds;
        magSize = maxAmmo;
        currentAmmo = magSize;
        reloadPercentage = Mathf.Round(magSize / 100 * reloadPercentage);
        bulletDelayTimer = bulletDelaySeconds;
        reloadWarning.GetComponent<Text>().enabled = false;
        //Debug.Log("Time till next shot " + bulletDelaySeconds);
    }
    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/Gun.cs
        BulletDelayTimer();
=======

        //BulletDelayTimer();
>>>>>>> Stashed changes:Project 51/Assets/Scripts/Gun.cs

        if (Input.GetButtonDown("Reload") && currentAmmo != magSize && outOfAmmo == false && reloading == false)
        {
<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/Gun.cs
=======
            //reloadDone = false;
>>>>>>> Stashed changes:Project 51/Assets/Scripts/Gun.cs
            Reload();
            Debug.Log("Manual Reload");
            //reloadTimer = true;

        }
        //Debug.Log(reloadPercentage);
        if (Input.GetButton("Fire") && reloadDone == true && outOfAmmo == false && bulletDelaySeconds <= bulletDelayTimer && playerUI.pause == false)
        {
            Shoot();
        }
        else
        {
            bulletDelaySeconds = maxBulletDelaySeconds;
        }
    }
    public void Reload()
    {
        //if (reloadTimer == true && reloadDelaySeconds >= reloadDelayTimer)
        //{
        //    reloadDelayTimer += Time.deltaTime;
        //    //Debug.Log("Time till next reload is done " + reloadDelayTimer + " Needs to = " + reloadDelaySeconds);
        //}
        //if (reloadDelayTimer >= reloadDelaySeconds)
        //{
        //playerAnimator.ReloadAnimation();
        maxAmmo -= magSize - currentAmmo;

        if (maxAmmo <= 0)
        {
            maxAmmo = 0;
        }

<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/Gun.cs
        currentAmmo = magSize;
        //Debug.Log("Reloaded " + currentAmmo + " rounds");
        //reloadNeeded = false;
        //reloadTimer = false;
        bulletDelayTimer = bulletDelaySeconds;
        //reloadDelayTimer = 0;
        reloadWarning.GetComponent<Text>().enabled = false;
        playerUI.UpdateUI();
        //}
    }
=======
    //public void BulletDelayTimer()
    //{
    //    if (bulletDelaySeconds >= bulletDelayTimer)
    //    {
    //        bulletDelayTimer += Time.deltaTime;
    //        bulletDelayTimer += Mathf.Round(Time.deltaTime * 10);
    //        //Debug.Log("Time till next shot " + bulletDelayTimer);
    //    }
    //}

>>>>>>> Stashed changes:Project 51/Assets/Scripts/Gun.cs

    public void Shoot()
    {
        Vector3 gunOrigin = new Vector3(camera.transform.position.x, camera.transform.position.y + raycastOffSetY, camera.transform.position.z);
        Debug.DrawRay(gunOrigin, camera.transform.forward * gunRange, Color.red);
        RaycastHit hit;
<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/Gun.cs
        //playerAnimator.ShootAnimation();
        MuzzelFlash();
        Ammo();
        bulletDelayTimer = 0;
        if (Physics.Raycast(gunOrigin, camera.transform.forward, out hit, gunRange, ~LayerMask.GetMask("ImpactEffect")))
        {
            GameObject ImpactEffect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactEffect, 0.2f);
            //Debug.Log(hit.transform.name);

            if (hit.collider.tag == "Enemy")
            {

                //Debug.Log("Fired");
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(gunDamage);
=======

        if (Physics.Raycast(gunOrigin, camera.transform.forward, out hit, gunRange, ~LayerMask.GetMask("ImpactEffect")) && shotFired == false)
        {
            if (delayOver == true)
            {
                //StopCoroutine(BulletDelayTimer());
                StopAllCoroutines();
                Ammo();
                MuzzelFlash();
                playerAnimator.ShootAnimation();
                GameObject ImpactEffect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ImpactEffect, 0.2f);
                //Debug.Log(hit.transform.name);

                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<EnemyHealth>().TakeDamage(gunDamage);
                }

                delayOver = false;
                Debug.Log("delayOver 1 " + delayOver);
                Debug.Log("Fired");

            }
            else
            {
            StartCoroutine(BulletDelayTimer());
>>>>>>> Stashed changes:Project 51/Assets/Scripts/Gun.cs
            }

        }
<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/Gun.cs
=======
        //bulletDelayTimer = 0;
    }

    IEnumerator BulletDelayTimer()
    {
        shotFired = true;
         yield return new WaitForSeconds(bulletDelaySeconds);
        if (bulletDelaySeconds >= chargeUpCap)
        {
            bulletDelaySeconds -= chargeUp;
            Debug.Log("delayOver 2 " + delayOver);
        }
        delayOver = true;
        shotFired = false;
        Debug.Log("Charging");
        Debug.Log("delayOver 3 " + delayOver);
>>>>>>> Stashed changes:Project 51/Assets/Scripts/Gun.cs
    }

    public void MuzzelFlash()
    {
        GameObject MuzzleFlash = Instantiate(muzzleFlash, spawnMuzzleFlash.position, spawnMuzzleFlash.rotation);
        Destroy(MuzzleFlash, 0.2f);
    }

    public void Ammo()
    {
<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/Gun.cs
        if (currentAmmo > 0)
        {
            currentAmmo--;
            playerUI.UpdateUI();
            //Debug.Log(currentAmmo);
        }
=======
        if (reloading == false)
        {
            playerAnimator.ReloadAnimation();
            //Debug.Log("Animation Playing");
        }
        //if (reloadTimer == true && reloadDelaySeconds >= reloadDelayTimer)
        //{
        //    reloadDelayTimer += Time.deltaTime;
        //    //Debug.Log("Time till next reload is done " + reloadDelayTimer + " Needs to = " + reloadDelaySeconds);
        //}
        //if (reloadDelayTimer >= reloadDelaySeconds)
        //{
        if (reloadDone == true)
        {
            //maxAmmo -= magSize - currentAmmo;
            //if (maxAmmo <= 0)
            //{
            //    maxAmmo = 0;
            //}

            currentAmmo = magSize;
            //Debug.Log("Reloaded " + currentAmmo + " rounds");
            //reloadNeeded = false;
            //reloadTimer = false;
            //bulletDelayTimer = bulletDelaySeconds;
            //reloadDelayTimer = 0;
            reloadWarning.GetComponent<Text>().enabled = false;
            playerUI.UpdateUI();
            reloadDone = false;
            reloading = false;
            //Debug.Log("Reloaded");

        }
        else
        {
            reloading = true;
            reloadDone = true;
            //Debug.Log("Reloading " + reloading);
            //Debug.Log("test");
        }

    }

    public void Ammo()
    {
>>>>>>> Stashed changes:Project 51/Assets/Scripts/Gun.cs
        if (currentAmmo == 0 && maxAmmo == 0)
        {
            outOfAmmo = true;
        }
<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/Gun.cs
        if (currentAmmo == 0 && maxAmmo > 0)
        {
            Reload();
            //reloadNeeded = true;
            //reloadTimer = true;
            //Debug.Log("Reload");
        }

        if (currentAmmo <= reloadPercentage)
        {
            reloadWarning.GetComponent<Text>().enabled = true;
=======

        if (currentAmmo <= reloadPercentage)
        {
            reloadWarning.GetComponent<Text>().enabled = true;
        }

        if (currentAmmo >= 1 && reloadDone == false)
        {
            currentAmmo--;
            playerUI.UpdateUI();
            //Debug.Log("shot");
        }

        if (currentAmmo == 0 && maxAmmo > 0 && reloading == false)
        {
            Reload();
            //reloadNeeded = false;
            //reloadTimer = true;
            //Debug.Log("Auto Reload");
>>>>>>> Stashed changes:Project 51/Assets/Scripts/Gun.cs
        }

    }
<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/Gun.cs
=======

>>>>>>> Stashed changes:Project 51/Assets/Scripts/Gun.cs
}
