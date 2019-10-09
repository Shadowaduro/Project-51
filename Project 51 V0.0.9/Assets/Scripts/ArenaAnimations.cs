using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaAnimations : MonoBehaviour
{
    Collider doors;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        doors = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        if (doors.isTrigger == true)
        {
            animator.SetBool("Door", true);
        }
    }

    void OnMouseExit()
    {
        animator.SetBool("Door", false);
    }
}
