using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponController : WeaponController
{
    public Transform muzzlePoint;
    public GameObject ammoPrefab;

    public float firingForce;


    public override void Fire()
    {
        GameObject newProjectile = Instantiate(ammoPrefab);
        newProjectile.transform.position = muzzlePoint.position;
        newProjectile.transform.rotation = muzzlePoint.rotation;

        Rigidbody rigidbody = newProjectile.GetComponent<Rigidbody>();
        rigidbody.AddForce(muzzlePoint.forward * firingForce);
    }
}
