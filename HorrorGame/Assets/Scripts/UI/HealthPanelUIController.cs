using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthPanelUIController : MonoBehaviour
{
    public Text healthPointsValueText;
    public SegmentProgressBarUIController progressBar;

    public Destructible trackedDestructible;

    public int lastShownHealth { private set; get; }


    protected virtual void Start()
    {
        if(progressBar)
        {
            progressBar.Generate(trackedDestructible.maxHealthPoints);
        }
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        if(trackedDestructible)
        {
            if (lastShownHealth != trackedDestructible.GetCurrentHealthPoints())
            {
                int newValue = trackedDestructible.GetCurrentHealthPoints();

                if (healthPointsValueText)
                {
                    healthPointsValueText.text = newValue.ToString();
                }

                if(progressBar)
                {
                    progressBar.UpdateToIndex(newValue);
                }

                lastShownHealth = newValue;
            }
        }
        else
        {
            if (lastShownHealth != 0)
            {
                if (healthPointsValueText)
                {
                    healthPointsValueText.text = "0";
                }

                if (progressBar)
                {
                    progressBar.UpdateToIndex(0);
                }

                lastShownHealth = 0;
            }
        }
    }
}
