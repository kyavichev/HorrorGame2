using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNPCController : MonoBehaviour
{
    public Animator animator;

    private float _scaredTimer = 0;
    private bool _isTalking = false;


    public void Update()
    {
        if(!_isTalking)
        {
            _scaredTimer -= Time.deltaTime;
            if(_scaredTimer <= 0)
            {
                _scaredTimer = Random.Range(10, 20);
                animator.SetTrigger("IsScared");
            }
        }
    }

    public void StartTalking()
    {
        _isTalking = true;
        animator.SetBool("IsInConversation", true);
    }


    public void StopTalking()
    {
        _isTalking = false;
        animator.SetBool("IsInConversation", false);
    }
}
