using UnityEngine;
using UnityEngine.AI;

// Simple Zombie AI
public class SimpleZombieAI : MonoBehaviour
{
    public enum ZombieState { Idle, Patrolling, Chase };
    public ZombieState state = ZombieState.Idle;

    public float detectionRadius = 20;
    public float attackRadius = 0.1f;
    public LayerMask targetMask;
    public float speed = 1;
    public float rotSpeed = 10;

    public NavMeshAgent agent;
    public Animator animator;


    public Transform[] patrolPoints;
    private int _patrolPointIndex = 0;


    // Update is called once per frame
    void Update()
    {
        bool moved = false;

        // Check if anything is detected
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, targetMask);
        if (colliders.Length > 0)
        {
            state = ZombieState.Chase;
        }
        else if (state != ZombieState.Idle && state != ZombieState.Patrolling)
        {
            state = ZombieState.Idle;
            if (animator)
            {
                animator.SetFloat("speed", 0);
                animator.ResetTrigger("attack");
            }
            return;
        }

        if (state == ZombieState.Idle)
        {
            if (patrolPoints.Length > 0)
            {
                state = ZombieState.Patrolling;
            }
        }
        else if (state == ZombieState.Patrolling)
        {
            float distance = Vector3.Distance(patrolPoints[_patrolPointIndex].position, transform.position);
            if (distance > 1)
            {
                Vector3 dir = (patrolPoints[_patrolPointIndex].position - transform.position).normalized;
                Vector3 moveStep = dir * speed * Time.deltaTime;
                agent.Move(moveStep);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
            }
            else
            {
                _patrolPointIndex++;
                _patrolPointIndex %= patrolPoints.Length;
            }

            moved = true;
        }
        else if (state == ZombieState.Chase)
        {
            Collider collider = colliders[0];
            float distance = Vector3.Distance(transform.position, collider.gameObject.transform.position);
            if (distance > attackRadius)
            {
                Vector3 dir = (collider.transform.position - transform.position).normalized;
                Vector3 moveStep = dir * speed * Time.deltaTime;
                agent.Move(moveStep);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);

                //if (animator != null)
                //{
                //    animator.SetFloat("speed", speed);
                //}

                moved = true;
            }
            else
            {
                if (animator != null)
                {
                    animator.SetTrigger("attack");
                }
            }
        }

        if (animator)
        {
            if (moved)
            {
                animator.SetFloat("speed", speed);
            }
            else
            {
                animator.SetFloat("speed", 0);
            }
        }

        //if (moved == false)
        //{
        //    if (animator != null)
        //    {
        //        animator.SetFloat("speed", 0);
        //    }
        //}
    }
}
