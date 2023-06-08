using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script detects if a collider enters the area and if collider is detected
// game object will orient towards the collider
public class OrientTowardsHero : MonoBehaviour
{
    public float detectionRadius = 20;
    public LayerMask targetMask;
    public float rotSpeed = 10;

    
    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, targetMask);
        if(colliders.Length > 0)
        {
            Collider collider = colliders[0];
            Vector3 dir = collider.transform.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
        } 
    }
}
