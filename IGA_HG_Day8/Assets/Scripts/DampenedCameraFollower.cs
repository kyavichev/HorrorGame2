using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampenedCameraFollower : MonoBehaviour
{
    public float damping = 0.05f;

    private Vector3 offset;
    private Vector3 velocity;

    public float rotationSpeed = 10;

    public GameObject objectToTrack;

    public bool matchTransformToObjectToTrack = false;


    // Start is called before the first frame update
    void Start()
    {
        if(matchTransformToObjectToTrack)
        {
            transform.position = objectToTrack.transform.position;
            transform.rotation = objectToTrack.transform.rotation;
        }

        offset = objectToTrack.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToTrack)
        {
            // Position
            Vector3 targetPosition = objectToTrack.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);

            // Rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, objectToTrack.transform.rotation, Time.deltaTime * rotationSpeed);
        }
    }
}
