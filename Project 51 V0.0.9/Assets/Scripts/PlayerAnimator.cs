using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    PlayerController playerController;
    float inputH;
    float inputV;

    //bool attacking;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.selectedClass == PlayerController.Class.Mech)
        {
            inputH = Input.GetAxis("Horizontal");
            inputV = Input.GetAxis("Vertical");

            animator.SetFloat("inputH", inputH);
            animator.SetFloat("inputV", inputV);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    animator.SetTrigger("Attacking");
        //    //attacking = true;
        //    Debug.Log("attacked");
        //}
    }

    public void ReloadAnimation()
    {
        animator.SetTrigger("Reload");
    }
    public void ShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }
    public void JumpAnimation()
    {
        animator.SetTrigger("Jump");
    }
}
