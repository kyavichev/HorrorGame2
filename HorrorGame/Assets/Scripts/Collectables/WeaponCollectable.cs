using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollectable : Collectable
{
    public GameObject weaponPrefab;


    public override void Use(Collector collector)
    {
        LoadoutController loadoutController = collector.GetComponent<LoadoutController>();
        if(loadoutController)
        {
            loadoutController.EquipRightHand(weaponPrefab, this);
        }
    }
}
