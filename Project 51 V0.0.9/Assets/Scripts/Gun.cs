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
    float bulletDelayTimer;

    [Space(5)]
    //public float reloadDelaySeconds = 1f;
    //float reloadDelayTimer;

    [Space(5)]
    //[HideInInspector]
    public bool reloadDone = true;
    bool reloadNeeded;
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
        currentAmmo = magSize;
        reloadPercentage = Mathf.Round(magSize / 100 * reloadPercentage);
        bulletDelayTimer = bulletDelaySeconds;
        reloadWarning.GetComponent<Text>().enabled = false;
        //Debug.Log("Time till next shot " + bulletDelaySeconds);
    }
    // Update is called once per frame
    void Update()
    {

        BulletDelayTimer();

        if (Input.GetButtonDown("Reload") && currentAmmo != magSize && outOfAmmo == false)
        {
            reloadDone = false;
            playerAnimator.ReloadAnimation();
            Reload();
            //reloadTimer = true;

        }
        //Debug.Log(reloadPercentage);
        if (Input.GetButton("Fire") && outOfAmmo == false && bulletDelaySeconds <= bulletDelayTimer && playerUI.pause == false)
        {
            if (reloadDone == false)
            {
                Shoot();
            }
        }
    }

    public void BulletDelayTimer()
    {
        if (bulletDelaySeconds >= bulletDelayTimer)
        {
            bulletDelayTimer += Time.deltaTime;
            bulletDelayTimer += Mathf.Round(Time.deltaTime * 10);
            //Debug.Log("Time till next shot " + bulletDelayTimer);
        }
    }


    public void Shoot()
    {
        Vector3 gunOrigin = new Vector3(camera.transform.position.x, camera.transform.position.y + raycastOffSetY, camera.transform.position.z);
        Debug.DrawRay(gunOrigin, camera.transform.forward * gunRange, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(gunOrigin, camera.transform.forward, out hit, gunRange, ~LayerMask.GetMask("ImpactEffect")))
        {
            Ammo();
            MuzzelFlash();
            playerAnimator.ShootAnimation();
            GameObject ImpactEffect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactEffect, 0.2f);
            //Debug.Log(hit.transform.name);

            if (hit.collider.tag == "Enemy")
            {
                //Debug.Log("Fired");
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(gunDamage);
            }
        }
        bulletDelayTimer = 0;
    }

    public void MuzzelFlash()
    {
        GameObject MuzzleFlash = Instantiate(muzzleFlash, spawnMuzzleFlash.position, spawnMuzzleFlash.rotation);
        Destroy(MuzzleFlash, 0.2f);
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
        if (reloadDone == true)
        {
            maxAmmo -= magSize - currentAmmo;

            if (maxAmmo <= 0)
            {
                maxAmmo = 0;
            }

            currentAmmo = magSize;
            //Debug.Log("Reloaded " + currentAmmo + " rounds");
            //reloadNeeded = false;
            //reloadTimer = false;
            bulletDelayTimer = bulletDelaySeconds;
            //reloadDelayTimer = 0;
            reloadWarning.GetComponent<Text>().enabled = false;
            playerUI.UpdateUI();
            reloadDone = false;
        }
        //else
        //{
        //    reloadDone = true;
        //    Debug.Log("test");
        //}
        //}
    }

    public void Ammo()
    {
        if (currentAmmo == 0 && maxAmmo == 0)
        {
            outOfAmmo = true;
        }

        if (currentAmmo <= reloadPercentage)
        {
            reloadWarning.GetComponent<Text>().enabled = true;
        }

        if (currentAmmo == 0 && maxAmmo > 0)
        {
            playerAnimator.ReloadAnimation();
            Reload();
            //reloadNeeded = true;
            //reloadTimer = true;
            //Debug.Log("Reload");
        }
        else if (currentAmmo > 0 && reloadDone == false)
        {
            currentAmmo--;
            playerUI.UpdateUI();
            Debug.Log("shot");
        }
    }
    
}
