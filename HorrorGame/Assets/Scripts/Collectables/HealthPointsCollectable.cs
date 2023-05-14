using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPointsCollectable : Collectable
{
    public int healAmount = 1;


    protected override void Collect(Collector collector)
    {
        Destructible destructible = collector.GetComponent<Destructible>();
        if (destructible != null)
        {
            if (destructible.IsHurt())
            {
                destructible.Heal(healAmount);

                base.Collect(collector);
            }
        }
    }
}
