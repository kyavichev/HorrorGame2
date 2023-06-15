using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomThingAnimationController : MonoBehaviour
{
    public Animator animator;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            animator.SetTrigger("RaiseArmsTrigger");
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("Jump", !animator.GetBool("Jump"));
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("ForceJump");
        }
    }
}
