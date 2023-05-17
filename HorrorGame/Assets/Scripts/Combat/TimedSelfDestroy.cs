using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSelfDestroy : MonoBehaviour
{
    public float duration = 5;
    public float timer { protected set; get; }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
            Destroy(gameObject);
        }
    }
}
