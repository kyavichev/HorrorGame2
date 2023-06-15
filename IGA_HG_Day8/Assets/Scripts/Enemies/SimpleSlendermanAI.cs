using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SimpleSlendermanAI : MonoBehaviour
{
    public enum State { Idle, Thinking, MovingCloser, Creeping, TeleportAway }
    public State state;

    public Animator animator;
    public NavMeshAgent agent;

    public LayerMask terrainMask;

    // Idle
    public float idleDuration = 3;
    public float idleTimer { private set; get; } = 0;

    // Creeping
    public GameObject targetObject;
    public float moveCloserDistance = 30f;

    public float stoppingDistance = 0.5f;

    public float creepDistance = 30;

    public bool isDetected = false;

    public float fleeSpeed = 10;

    private Vector3 _startPosition;


    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude);

        switch(state)
        {
            case State.Idle:
                UpdateIdle();
                break;

            case State.Thinking:
                UpdateThink();
                break;

            case State.MovingCloser:
                UpdateMovingCloser();
                break;

            case State.Creeping:
                UpdateCreeping();
                break;

            case State.TeleportAway:
                UpdateTeleportAway();
                break;
        }
    }


    private void UpdateIdle()
    {
        if(isDetected)
        {
            idleTimer = 0;
            state = State.TeleportAway;
            return;
        }

        idleTimer += Time.deltaTime;
        if(idleTimer >= idleDuration)
        {
            idleTimer = 0;

            state = State.Thinking;
        }
    }


    private void UpdateThink()
    {
        if(targetObject == null)
        {
            state = State.Idle;
            return;
        }

        if (isDetected)
        {
            //isDetected = false;
            state = State.TeleportAway;
        }
        else
        {
            float distance = Vector3.Distance(transform.position, targetObject.transform.position);
            if (distance > moveCloserDistance)
            {
                agent.destination = targetObject.transform.position;
                agent.isStopped = false;
                state = State.MovingCloser;
            }
            else
            {
                state = State.Idle;
            }
        }
    }


    private void UpdateMovingCloser()
    {
        if (isDetected)
        {
            state = State.Thinking;
            return;
        }

        if (agent.isStopped)
        {
            state = State.Idle;
            return;
        }

        if(agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            agent.isStopped = true;
            state = State.Idle;
            return;
        }

        if(agent.remainingDistance < stoppingDistance)
        {
            agent.isStopped = true;
            state = State.Idle;
            return;
        }
    }


    private void UpdateCreeping()
    {
    }



    private void UpdateTeleportAway()
    {
        Vector3 dir = (transform.position - targetObject.transform.position).normalized;
        dir.y = 0;
        Vector3 targetPosition = dir * 30;
        targetPosition += new Vector3(0, 100, 0);
        RaycastHit hitInfo;

        int checkCount = 0;
        int maxCheckCount = 10;
        float randomRadius = 0;
        float randomRadiusIncrease = 5;
        while (checkCount < maxCheckCount)
        {
            checkCount++;
            //Debug.Log($"Check number {checkCount}");

            float angle = Random.Range(0, Mathf.PI * 2);
            Vector3 offset = new Vector3(Mathf.Sin(angle) * randomRadius, 0, Mathf.Cos(angle) * randomRadius);
            Vector3 checkPosition = targetPosition + offset;
            //Debug.Log($"Checking: {checkPosition}");

            if (Physics.Raycast(checkPosition, new Vector3(0, -1, 0), out hitInfo, 200, terrainMask))
            {
                //Debug.Log($"Found point on the ground {hitInfo.point}");

                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(hitInfo.point, path))
                {
                    agent.enabled = false;
                    transform.position = hitInfo.point;
                    agent.enabled = true;
                    agent.SetDestination(hitInfo.point);

                    //Debug.Log("Teleport!");
                    Debug.DrawLine(targetPosition, hitInfo.point, Color.green, 3);

                    isDetected = false;
                    state = State.Idle;

                    break;
                }
                else
                {
                    //Debug.Log("Could not find path here!");
                    Debug.DrawLine(targetPosition, hitInfo.point, Color.red, 3);
                }
            }
            else
            {
                //Debug.Log("Could not find teleport location");

                Debug.DrawLine(targetPosition, targetPosition + new Vector3(0, -200, 0), Color.yellow);
            }

            randomRadius += randomRadiusIncrease;
        }
    }
}
