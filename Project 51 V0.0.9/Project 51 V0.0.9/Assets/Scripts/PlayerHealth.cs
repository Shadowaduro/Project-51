using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
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

    public int healAmount = 30;
    public int damage = 20;

    [Tooltip("The Health Bar")]
    public Image healthBar;
    [Tooltip("The Health Bar that is delayed before going down")]
    public Image healthBarBackground;
    //[Tooltip("The Health Percentage")]
    //public Text healthText;
    //[Tooltip("The UI eliment with the animation on it")]
    //public Animator animator;

    //Achievements achievement;

    [Header("----------------------------------------------------------------")]
    [Header("Health % Colors")]
    [Header("----------------------------------------------------------------")]
    
    [Tooltip("Color of the Health between 100-75%")]
    [Space(5)]
    public Color full = new Color (0.3f, 1f, 0f, 1f);
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
    bool healthingPressed;
    Color tColor;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.color = full;
        tColor = full;
        //achievement = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Achievements>();
        currentHealthImage = maxHealth;
        healthTakeSpeed = healthTakeSpeed * 10;
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

                //healthText.text = currentHealthImage.ToString("F0") + "%";

                if (currentHealthImage <= currentHealth)
                {
                    currentHealthImage = currentHealth;
                    //healthText.text = currentHealthImage.ToString("F0") + "%";
                    damageTaken = false;
                }
            }
            else if (healing == true)
            {
                currentHealthImage += Time.deltaTime * healthTakeSpeed;
                currentHealth += Time.deltaTime * healthTakeSpeed;

                //healthText.text = currentHealthImage.ToString("F0") + "%";

                if (currentHealth >= healingAmount)
                {
                    currentHealth = healingAmount;
                    currentHealthImage = currentHealth;
                    //healthText.text = currentHealthImage.ToString("F0") + "%";
                    healing = false;
                }

            }
        }

        if (currentHealth < 1f)
        {
            currentHealth = 0f;
            currentHealthImage = 0f;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            currentHealthImage = maxHealth;
        }

        if (currentHealth > 25f && currentHealth != maxHealth)
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

        if (Input.GetKeyDown(KeyCode.H) && currentHealth < maxHealth)
        {
<<<<<<< Updated upstream:Project 51 V0.0.9/Project 51 V0.0.9/Assets/Scripts/PlayerHealth.cs
            // Starts healing
            healthingPressed = true;
            //sends healing amount to TakeDamage
            TakeDamage(maxHealth);
=======
            //Debug.Log("sToP");
            StopCoroutine(HealthUpdate());
>>>>>>> Stashed changes:Project 51/Assets/Scripts/PlayerHealth.cs
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            //sends damage amount to TakeDamage
            TakeDamage(damage);
        }

        /// ^^^^^^^^^^
        /// Can Remove
        ///
    }

    public void TakeDamage(float damage)
    {
        if (healthingPressed == true)
        {
            healingAmount = damage + currentHealth;

            if (healingAmount > maxHealth)
            {
                healingAmount = maxHealth;
            }

            healing = true;
            healthingPressed = false;
        }
        else if (currentHealth >= 0f)
        {
            StartCoroutine(OnDamage(timeTillDamage));
            //animator.SetTrigger("OnDamage");
            currentHealth -= damage;

            //achivements
            //achievement.totalDamageTaken += damage;
            //achievement.DamageAchivement();
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
