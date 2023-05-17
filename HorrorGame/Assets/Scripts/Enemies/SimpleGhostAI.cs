using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGhostAI : MonoBehaviour
{
    // Behavior Settings
    public float hoverHeight = 1;
    public float observeDistance = 5;

    // Movement settings
    public float speed = 1;
    public float rotSpeed = 10;
    public LayerMask terrainMask;

    public GameObject targetObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObject == null)
        {
            return;
        }

        Vector3 dir = (targetObject.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);
        if(distance > observeDistance)
        {
            Vector3 moveStep = dir * speed * Time.deltaTime;
            Vector3 newPosition = transform.position + moveStep;

            Vector3 checkPosition = newPosition + new Vector3(0, 100, 0);
            RaycastHit hitInfo;
            if (Physics.Raycast(checkPosition, new Vector3(0, -1, 0), out hitInfo, 200, terrainMask))
            {
                float y = hitInfo.point.y + hoverHeight;
                newPosition.y = y;
            }

            transform.position = newPosition;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);

        
    }
}
