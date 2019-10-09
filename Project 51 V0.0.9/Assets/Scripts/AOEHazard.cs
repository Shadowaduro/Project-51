using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEHazard : MonoBehaviour
{
    public float damage;
    public float timeTillDamage;
    public float damageArea;

    float playerCurrentTime, EnemyCurrentTime;

    // Start is called before the first frame update
    void Start()
    {
        damageArea = GetComponent<SphereCollider>().radius = damageArea;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyCurrentTime += Time.deltaTime;

            if (EnemyCurrentTime >= timeTillDamage)
            {
                other.GetComponent<EnemyHealth>().TakeDamage(damage);
                EnemyCurrentTime = 0;
            }
        }

        if (other.tag == "Player")
        {
            playerCurrentTime += Time.deltaTime;

            if (playerCurrentTime >= timeTillDamage)
            {
                Debug.LogError("Player needs PlayerHealth script");
                playerCurrentTime = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyCurrentTime = 0;
        }

        if (other.tag == "Player")
        {
            playerCurrentTime = 0;
        }
    }
}
