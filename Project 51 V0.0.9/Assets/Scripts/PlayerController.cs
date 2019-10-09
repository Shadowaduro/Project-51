using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public PlayerUI gameEndCheck;

    public enum Class { Ketch, Mech };
    public Class selectedClass;

    public float speed = 8f;
    public float sprintSpeed = 8f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    float dashPower;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private PlayerAnimator playerAnimator;
    private int jumps;

    public float maxDashDis = 10f;
    public float dashTime = 16;

    float currentDashDis;
    bool dashing;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        currentDashDis = maxDashDis;
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt) && selectedClass == PlayerController.Class.Mech)
            {
                dashing = true;
                playerAnimator.animator.SetTrigger("Dash");
            }


            ///
            /// Remove when game is done!
            /// VVVVVVVVVVVVVVVVVVVVVVVV

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed += sprintSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed -= sprintSpeed;
            }
            /// ^^^^^^^^^^^^^^^^^^^^^^^^
            ///

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") + dashPower);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButtonDown("Jump"))
            {
                playerAnimator.JumpAnimation();
                moveDirection.y = jumpSpeed;
            }
            jumps = 0;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed += sprintSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed -= sprintSpeed;
            }
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical") + dashPower);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;

            if (Input.GetButtonDown("Jump") && jumps < 1 && selectedClass == PlayerController.Class.Ketch)
            {
                moveDirection.y = jumpSpeed;
                jumps++;
            }
        }

        if (currentDashDis < 0)
        {
            dashing = false;
        }

        if (dashing == true)
        {
            dashPower = currentDashDis;
        }
        else
        {
            dashPower = 0;
            currentDashDis = maxDashDis;
        }

        if (dashing)
        {
            moveDirection.y -= gravity * 2 * Time.deltaTime;
            currentDashDis -= Time.deltaTime * dashTime;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        controller.Move(moveDirection * Time.deltaTime);
    }
}
