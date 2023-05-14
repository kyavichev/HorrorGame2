using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public List<Collectable> collectedCollectables;


    void Awake()
    {
        collectedCollectables = new List<Collectable>();
    }


    public void Collect(Collectable collectable)
    {
        //MessageBoxUIController.GetInstance().ShowMessage(collectable.name);

        if(collectable.collectableType == Collectable.CollectableType.Consumable)
        {
            // Increase jump boost
            collectable.gameObject.SetActive(false);
            Destroy(collectable.gameObject);
        }
        else if(collectable.collectableType == Collectable.CollectableType.Storable)
        {
            collectedCollectables.Add(collectable);
            collectable.gameObject.transform.SetParent(transform);
            collectable.gameObject.SetActive(false);
        }
    }


    public int GetItemCount(string collectableName)
    {
        int count = 0;

        foreach(Collectable collectable in collectedCollectables)
        {
            if(collectable.collectableName == collectableName)
            {
                count++;
            }
        }

        return count;
    }
}
