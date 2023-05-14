using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotLightScanner : MonoBehaviour
{
    public LayerMask layerMask;

    public new Light light;


    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        int numRaycasts = 10;

        for(int i = 0; i < numRaycasts; i++)
        {
            float angle = -light.spotAngle * 0.5f + (light.spotAngle / numRaycasts) * i;
            Vector3 dir = Quaternion.AngleAxis(angle, transform.up) * transform.forward;
            if (Physics.Raycast(transform.position, dir, out hitInfo, light.range, layerMask))
            {
                Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                ProcessHitInfo(hitInfo);
            }
        }
    }


    private void ProcessHitInfo(RaycastHit hitInfo)
    {
        SimpleSlendermanAI ai = hitInfo.collider.gameObject.GetComponent<SimpleSlendermanAI>();
        if(ai)
        {
            ai.isDetected = true;
        }
    }
}
