using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public List<Collectable> collectedCollectables;

    public AudioSource onCollectAudioSource;


    void Awake()
    {
        collectedCollectables = new List<Collectable>();
    }


    public void Collect(Collectable collectable)
    {
        MessageBoxUIController.GetInstance().ShowMessage(collectable.name);

        if(collectable.collectableType == Collectable.CollectableType.Consumable)
        {
            // Consume the item

            // Delete the item
            collectable.gameObject.SetActive(false);
            Destroy(collectable.gameObject);

            // Play audio FX
            onCollectAudioSource.Play();
        }
        else if(collectable.collectableType == Collectable.CollectableType.Storable)
        {
            // Hide the item and add it to the hero (both list and game object)
            collectedCollectables.Add(collectable);
            collectable.gameObject.transform.SetParent(transform);
            collectable.gameObject.SetActive(false);

            // Play audio FX
            onCollectAudioSource.Play();
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


    public void RemoveCollectable(Collectable collectableToRemove)
    {
        if(collectedCollectables.Contains(collectableToRemove))
        {
            collectedCollectables.Remove(collectableToRemove);
            Destroy(collectableToRemove.gameObject);
        }
    }
}
