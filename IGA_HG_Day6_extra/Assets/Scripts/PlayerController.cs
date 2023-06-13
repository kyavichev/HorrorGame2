using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float rotationSpeed = 1;

    protected float currentSpeed;

    public NavMeshAgent agent;
    public Animator animator;

    protected int moveDir = 0; // 1 forward, -1 backwards
    protected int rotDir = 0; // 1 is right, -1 is left


    // Update is called once per frame
    void Update()
    {
        bool hasMoved = false;
        currentSpeed = 0;

        if (moveDir != 0)
        {
            currentSpeed = speed * Time.deltaTime * moveDir;
            Vector3 offset = transform.forward * currentSpeed;
            agent.Move(offset);

            moveDir = 0;
            hasMoved = true;

            if (animator)
            {
                animator.SetFloat("speed", speed);
            }
        }

        if(hasMoved == false)
        {
            if(animator)
            {
                animator.SetFloat("speed", 0);
            }
        }

        if(rotDir != 0)
        {
            transform.Rotate(new Vector3(0, rotDir, 0) * rotationSpeed * Time.deltaTime);
            rotDir = 0;
        }
    }


    public void MoveForward()
    {
        moveDir = 1;
    }


    public void MoveBack()
    {
        moveDir = -1;
    }


    public void RotateLeft()
    {
        rotDir = -1;
    }

    public void RotateRight()
    {
        rotDir = 1;
    }
}
