using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyBehaviour { chaseTarget, attackTarget, charge, stop }

    public EnemyBehaviour currentBehaviour = EnemyBehaviour.chaseTarget;
    public Transform target;
    public bool chasing = false;
    public float enemyDamage = 5f;
    [HideInInspector]
    public float distToTarget;
    [HideInInspector]
    public NavMeshAgent agent;

    // Start is called before the first frame update
    public virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        CallBehaviour(EnemyBehaviour.chaseTarget);
    }
    // Update is called once per frame
    public virtual void Update()
    {
        if(target != null)
        {
            distToTarget = Vector3.Distance(transform.position, target.position);
        }

       
        if (currentBehaviour == EnemyBehaviour.chaseTarget)
        {
            if (chasing == true)
            {
                if (distToTarget >= 2f)
                {
                    MoveToTarget(target);
                }
                else
                {
                    CallBehaviour(EnemyBehaviour.attackTarget);
                }
            }
        }
    }

    public virtual void CallBehaviour(EnemyBehaviour behaviour)
    {
        currentBehaviour = behaviour;
       
        if (behaviour == EnemyBehaviour.chaseTarget)
        {
            if (chasing == false)
            {
                chasing = true;
                agent.isStopped = false;
            }
        }
        else if (behaviour == EnemyBehaviour.attackTarget)
        {
            chasing = false;
            if (target != null)
            {
                if (agent.remainingDistance < 2f)
                {
                    agent.isStopped = true;
                    LookAtTarget(target);
                    if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f))
                    {
                        Debug.DrawRay(transform.position, transform.forward * 2f, Color.green, 1);
                        if (hit.collider.tag == "Player")
                        {
                            StartCoroutine(Attack(hit.collider));
                        }
                        CallBehaviour(EnemyBehaviour.chaseTarget);
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, transform.forward * 2f, Color.red, 1);
                        CallBehaviour(EnemyBehaviour.chaseTarget);
                    }
                }
                else
                {
                    CallBehaviour(EnemyBehaviour.chaseTarget);
                }
            }
        }
    }

    public virtual IEnumerator Attack(Collider coll)
    {
        //coll.transform.GetComponent<CharacterStats>().TakeDamage(enemyDamage);
        yield return new WaitForSeconds(1);
        CallBehaviour(EnemyBehaviour.chaseTarget);
    }

    public virtual void MoveToTarget(Transform t)
    {
        agent.SetDestination(t.position);
        LookAtTarget(t);
    }

    public virtual void LookAtTarget(Transform t)
    {
        transform.LookAt(t.position);
    }
}
