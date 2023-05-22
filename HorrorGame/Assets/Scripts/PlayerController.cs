using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float rotationSpeed = 1;

    public float currentSpeed { protected set; get; }

    public NavMeshAgent agent;
    public Animator animator;
    public LoadoutController loadoutController;

    protected int moveDir = 0; // 1 forward, -1 backwards
    protected int rotDir = 0; // 1 is right, -1 is left
    protected bool doAttack = false;


    void Start()
    {
        if (loadoutController == null)
        {
            loadoutController = GetComponent<LoadoutController>();
        }
    }


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

            if(animator)
            {
                animator.SetFloat("speed", currentSpeed);
            }

            moveDir = 0;
            hasMoved = true;
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

        if(doAttack)
        {
            if (loadoutController.rightHandWeapon == null)
            {
                doAttack = false;
            }
            else
            {
                if (loadoutController.rightHandWeapon.isMelee)
                {
                    if (animator)
                    {
                        animator.SetTrigger("attack");
                    }
                }
                else
                {
                    if(animator)
                    {
                        animator.SetTrigger("fireWeapon");
                    }

                    loadoutController.rightHandWeapon.Fire();
                }

                doAttack = false;
            }
        }
    }


    public float GetCurrentSpeed()
    {
        return this.currentSpeed;
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


    public void Attack()
    {
        doAttack = true;
    }
}
