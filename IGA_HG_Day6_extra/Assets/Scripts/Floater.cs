using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floater : MonoBehaviour
{
    public float floatAmount = 0.25f;
    public float floatSpeed = 1f;
    public bool floatsOnX = false;

    protected float originalPos;

    private float timeOffset = 0;

    public void Start()
    {
        originalPos = floatsOnX ? transform.localPosition.x : transform.localPosition.y;
        timeOffset = Random.Range(0f, 14.234f);
    }

    public void Update()
    {
        float t = Time.realtimeSinceStartup * floatSpeed + timeOffset;
        Vector3 pos = transform.localPosition;
        float newPosComponent = Mathf.Sin(t) * floatAmount + originalPos;
        if ( floatsOnX )
        {
            pos.x = newPosComponent;
        }
        else
        {
            pos.y = newPosComponent;
        }
        transform.localPosition = pos;
    }
}