using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackTrigger : MonoBehaviour
{
    public Destructor destructor;


    private void OnTriggerEnter(Collider other)
    {
        Destructible destructible = other.gameObject.GetComponent<Destructible>();
        if(destructible)
        {
            if (destructible.team != destructor.team)
            {
                Debug.Log("Attack");
                destructible.TakeDamage(destructor.damagePoints);
            }
        }
    }
}
