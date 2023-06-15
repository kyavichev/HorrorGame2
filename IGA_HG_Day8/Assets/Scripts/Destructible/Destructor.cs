using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// a Destructor causes damage to a Destructible when they touch
public class Destructor : MonoBehaviour
{
    public int damagePoints = 1;
    public Team team;

    
    // This function gets called whenever a collision event happens
    // Unity requires 2 colliders and at least one rigid body that is not kinematic
    private void OnCollisionEnter(Collision collision)
    {
        Destructible destructible = collision.collider.gameObject.GetComponent<Destructible>();
        if(destructible)
        {
            if (destructible.team != team)
            {
                destructible.TakeDamage(damagePoints);
            }
        }
    }


    // This function gets called whenever collider set as a trigger hits another collider
    private void OnTriggerEnter(Collider other)
    {
        Destructible destructible = other.gameObject.GetComponent<Destructible>();
        if (destructible)
        {
            if (destructible.team != team)
            {
                destructible.TakeDamage(damagePoints);
            }
        }
    }
}
