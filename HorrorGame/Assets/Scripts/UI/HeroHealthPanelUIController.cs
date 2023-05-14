using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealthPanelUIController : HealthPanelUIController
{
    protected override void Start()
    {
        GetHeroDestructible();

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetHeroDestructible();

        base.Update();
    }


    protected virtual void GetHeroDestructible()
    {
        if (trackedDestructible == null)
        {
            GameObject hero = GameManager.GetInstance().hero;
            if (hero)
            {
                trackedDestructible = hero.GetComponent<Destructible>();
            }
        }
    }
}
