using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadoutController : MonoBehaviour
{
    public MountPoint leftHand;
    public MountPoint rightHand;

    public WeaponController leftHandWeapon;
    public WeaponController rightHandWeapon;
    public Collectable leftHandItem;
    public Collectable rightHandItem;


    public void EquipRightHand(GameObject weaponPrefab, Collectable weaponItem)
    {
        if(rightHandWeapon != null)
        {
            Destroy(rightHandWeapon.gameObject);
            rightHandWeapon = null;
            rightHandItem = null;
        }

        GameObject newWeapon = Instantiate(weaponPrefab);
        newWeapon.transform.SetParent(rightHand.transform, false);

        WeaponController weaponController = newWeapon.GetComponent<WeaponController>();

        rightHandWeapon = weaponController;
        rightHandItem = weaponItem;
    }


    public void EquipLeftHand(GameObject weaponPrefab, Collectable weaponItem)
    {
        if (leftHandWeapon != null)
        {
            Destroy(leftHandWeapon.gameObject);
            leftHandWeapon = null;
            leftHandItem = null;
        }

        GameObject newWeapon = Instantiate(weaponPrefab);
        newWeapon.transform.SetParent(rightHand.transform, false);

        WeaponController weaponController = newWeapon.GetComponent<WeaponController>();

        leftHandWeapon = weaponController;
        leftHandItem = weaponItem;
    }
}
