using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float damage;

    Animator animator;
    EnemyHealth enemyHealth;
    PlayerUI playerUI;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        playerUI = GameObject.Find("UI").GetComponent<PlayerUI>();
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerUI.pause == false)
        {
            animator.SetTrigger("Attacking");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

        }
        else if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}