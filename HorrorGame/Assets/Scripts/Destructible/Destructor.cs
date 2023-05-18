using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// a Destructor causes damage to a Destructible when they touch

public class Destructor : MonoBehaviour
{
    public int damagePoints = 1;
    public Team team;


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destructible destructible = collision.collider.gameObject.GetComponent<Destructible>();
    //    if(destructible)
    //    {
    //        if (destructible.team != team)
    //        {
    //            destructible.TakeDamage(damagePoints);
    //        }
    //    }
    //}


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
