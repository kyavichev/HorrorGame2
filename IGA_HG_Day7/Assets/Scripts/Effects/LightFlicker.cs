using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public new Light light;

    public float minIntensity = 0.6f;
    public float maxIntensity = 1.5f;
    public float speed = 1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PerlinNoise(Time.time * speed, 0.0f);
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        light.intensity = intensity;
    }
}
