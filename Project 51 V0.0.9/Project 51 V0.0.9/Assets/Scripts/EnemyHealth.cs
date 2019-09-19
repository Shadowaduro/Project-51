using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    [Tooltip("The amount of health taken per second as a multiplyer")]
    [Range(0, 10)]
    public int healthTakeSpeed = 2;
    [Tooltip("The rate color change on low health")]
    [Range(0, 1)]
    public float lowHealthFlickRate = 0.5f;
    [Tooltip("The delay before the damage starts to take on the screen in seconds. Set value to 0 for no delay. (Visual damage Only!)")]
    [Range(0, 1)]
    public float timeTillDamage = 0.1f;

    //public int healAmount = 30;

    [Tooltip("The Health Bar")]
    public Image healthBar;
    [Tooltip("The Health Bar that is delayed before going down")]
    public Image healthBarBackground;
    [Tooltip("The Health Bar Border that goes behind everything")]
    public Image healthBarBackgroundBorder;

    [Header("----------------------------------------------------------------")]
    [Header("Health % Colors")]
    [Header("----------------------------------------------------------------")]

    [Tooltip("Color of the Health between 100-75%")]
    [Space(5)]
    public Color full = new Color(0.3f, 1f, 0f, 1f);
    [Tooltip("Color of the Health at 75-50%")]
    public Color half = new Color(1f, 0.9f, 0f, 1f);
    [Tooltip("Color of the Health at 50-30%")]
    public Color quarter = new Color(1f, 0.5f, 0f, 1f);
    [Tooltip("The Colors when on low health (20%)")]
    public Color thisTo = new Color(0.7f, 0.05f, 0.05f, 1f), that = new Color(1f, 1f, 1f, 0.3f);
    [Tooltip("When the player takes damage")]
    public Color whenHit = new Color(1f, 1f, 1f, 0.7f);

    float currentHealthImage;
    float healingAmount;
    float t;
    bool damageTaken = false;
    bool healing = false;
    bool healingTriggered;
    Color tColor;
    Vector3 spawnLoc;
    Music music;

    public GameObject enemy;

    void Start()
    {
        music = GameObject.Find("Music").GetComponent<Music>();
        spawnLoc = transform.position;
        currentHealth = maxHealth;
        healthBar.color = full;
        tColor = full;
        currentHealthImage = maxHealth;
        if (healthTakeSpeed < 10)
        {
            healthTakeSpeed = healthTakeSpeed * 10;
        }
    }

    void Update()
    {
        if (currentHealth <= maxHealth)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
            healthBarBackground.fillAmount = currentHealthImage / maxHealth;

            if (damageTaken == true)
            {
                healing = false;
                currentHealthImage -= Time.deltaTime * healthTakeSpeed;

                if (currentHealthImage <= currentHealth)
                {
                    currentHealthImage = currentHealth;
                    damageTaken = false;
                }
            }
            else if (healing == true)
            {
                currentHealthImage += Time.deltaTime * healthTakeSpeed;
                currentHealth += Time.deltaTime * healthTakeSpeed;

                if (currentHealth >= healingAmount)
                {
                    currentHealth = healingAmount;
                    currentHealthImage = currentHealth;
                    healing = false;
                }

            }
        }

        if (currentHealth <= 0f)
        {
            music.totalKills++;
            music.currentTime = 0;
            music.GotKill();
            Vector3 s = new Vector3(spawnLoc.x, spawnLoc.y, spawnLoc.z);
            GameObject Enemy = Instantiate(enemy, s, Quaternion.identity);
            Enemy.name = enemy.name;
            Destroy(this.gameObject);
            //healthingPressed  = true;
            //TakeDamage(maxHealth);
        }
        //if (currentHealth <= 0.01f)
        //{
        //    currentHealth = 0f;
        //    currentHealthImage = 0f;
        //}

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            currentHealthImage = maxHealth;
        }

        if (currentHealth > 20f && currentHealth != maxHealth && healing == true || damageTaken == true)
        {
            t += Time.deltaTime;
        }

        if (healing == true)
        {
            if (currentHealth >= 75f)
            {
                //                                      Green
                healthBar.color = Color.Lerp(tColor, full, t);
                tColor = healthBar.color;
                t = 0;
            }
            else if (currentHealth <= 75f && currentHealth > 50f)
            {
                //                                    Yellow
                healthBar.color = Color.Lerp(tColor, half, t);
                tColor = healthBar.color;
                t = 0;
            }
            else if (currentHealth <= 50f && currentHealth > 25f)
            {
                //                                    Orange
                healthBar.color = Color.Lerp(tColor, quarter, t);
                tColor = healthBar.color;
                t = 0;
            }
        }
        else if (damageTaken == true)
        {
            if (currentHealth > 75f)
            {
                healthBar.color = tColor;
                tColor = healthBar.color;
            }
            else if (currentHealth <= 75f && currentHealth > 50f)
            {
                //                                    Yellow
                healthBar.color = Color.Lerp(tColor, half, 0.05f);
                tColor = healthBar.color;
                t = 0;
            }
            else if (currentHealth <= 50f && currentHealth > 30f)
            {
                //                                    Orange
                healthBar.color = Color.Lerp(tColor, quarter, 0.05f);
                tColor = healthBar.color;
                t = 0;
            }
            else if (currentHealth <= 30)
            {
                //                                      Red
                healthBar.color = Color.Lerp(tColor, thisTo, 0.05f);
                tColor = healthBar.color;
                t = 0;
            }
        }

        if (currentHealth <= 20)
        {
            //                              Red                               White
            healthBar.color = Color.Lerp(thisTo, that, Mathf.PingPong(Time.time, lowHealthFlickRate));
        }

        ///
        /// Can Remove
        /// vvvvvvvvvv

        //if (Input.GetKeyDown(KeyCode.H) && currentHealth < maxHealth)
        //{
        //    // Starts healing
        //    healthingPressed = true;
        //    //sends healing amount to TakeDamage
        //    TakeDamage(healAmount);
        //}

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    //sends damage amount to TakeDamage
        //    TakeDamage(damage);
        //}

        /// ^^^^^^^^^^
        /// Can Remove
        ///
    }

    public void HealthInvisable(bool toggle)
    {
        healthBar.enabled = toggle;
        healthBarBackground.enabled = toggle;
        healthBarBackgroundBorder.enabled = toggle;
    }

    //public void HealthOff()
    //{
    //    healthBar.enabled = false;
    //    healthBarBackground.enabled = false;
    //    healthBarBackgroundBorder.enabled = false;
    //}


    public void TakeDamage(float damage)
    {
        if (healingTriggered == true)
        {
            healingAmount = currentHealth + damage;

            if (healingAmount > maxHealth)
            {
                healingAmount = maxHealth;
            }

            healing = true;
            healingTriggered = false;
        }
        else if (currentHealth >= 0f)
        {
            StartCoroutine(OnDamage(timeTillDamage));
            //animator.SetTrigger("OnDamage");
            currentHealth -= damage;
        }
    }

    IEnumerator OnDamage(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        damageTaken = true;
    }

    public void OnDamage()
    {
        Color t;
        t = healthBar.color;
        //                  White
        healthBar.color = whenHit;
        StartCoroutine(ForTheShitAbove(timeTillDamage, t));
    }

    IEnumerator ForTheShitAbove(float waitTime, Color c)
    {
        yield return new WaitForSeconds(waitTime);
        healthBar.color = c;
    }



}
