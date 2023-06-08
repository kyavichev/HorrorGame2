using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitCollectable : Collectable
{
    public int healAmount = 1;


    public override void Use(Collector collector)
    {
        Destructible destructible = collector.GetComponent<Destructible>();
        if (destructible != null)
        {
            if (destructible.IsHurt())
            {
                destructible.Heal(healAmount);

                collector.RemoveCollectable(this);
            }
        }
    }
}
