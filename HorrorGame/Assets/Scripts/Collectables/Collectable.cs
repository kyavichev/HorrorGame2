using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Collectable : MonoBehaviour
{
    public enum CollectableType
    {
        Consumable,
        Storable,
    }

    public CollectableType collectableType;
    public string collectableName;


    protected virtual void Collect(Collector collector)
    {
        collector.Collect(this);
    } 


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collected with " + collision.gameObject.name);

        Collector collector = collision.gameObject.GetComponent<Collector>();
        if(collector)
        {
            Collect(collector);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collected with " + collision.gameObject.name);

        Collector collector = collision.collider.gameObject.GetComponent<Collector>();
        if (collector)
        {
            Collect(collector);
        }
    }


    public virtual void Use(Collector collector)
    {
        Debug.Log("Use " + collectableName);
    }
}
