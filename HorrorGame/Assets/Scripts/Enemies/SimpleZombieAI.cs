using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SimpleZombieAI : MonoBehaviour
{
    public float detectionRadius = 20;
    public float attackRadius = 0.1f;
    public LayerMask targetMask;
    public float speed = 1;
    public float rotSpeed = 10;

    public NavMeshAgent agent;
    public Animator animator;


    // Update is called once per frame
    void Update()
    {
        bool moved = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, targetMask);
        if(colliders.Length > 0)
        {
            Collider collider = colliders[0];
            float distance = Vector3.Distance(transform.position, collider.gameObject.transform.position);
            if(distance > attackRadius)
            {
                Vector3 dir = (collider.transform.position - transform.position).normalized;
                Vector3 moveStep = dir * speed * Time.deltaTime;
                agent.Move(moveStep);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);

                animator.SetFloat("speed", speed);

                moved = true;
            }
            else
            {
                animator.SetTrigger("attack");
            }
        }

        if(moved == false)
        {
            animator.SetFloat("speed", 0);
        }
    }
}
