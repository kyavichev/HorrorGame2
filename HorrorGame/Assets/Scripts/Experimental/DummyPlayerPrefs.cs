using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDataItem
{
    public string name;
    public string description;
    public int level;
}

[System.Serializable]
public class SavedData
{
    public string name;
    public string something;
    public int count;

    public SaveDataItem[] items;
}

public class SaveDataKeys
{
    public const string VisitedKey = "visited";
    public const string SaveDataKey = "save_data";
}

public class DummyPlayerPrefs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.GetInstance().hero)
        {
            int visitCount = PlayerPrefs.GetInt(SaveDataKeys.VisitedKey, 0);
            visitCount++;
            PlayerPrefs.SetInt(SaveDataKeys.VisitedKey, visitCount);

            Debug.Log("Visited count: " + visitCount);

            SavedData savedData = null;
            if (PlayerPrefs.HasKey(SaveDataKeys.SaveDataKey))
            {
                string savedString = PlayerPrefs.GetString(SaveDataKeys.SaveDataKey);
                savedData = JsonUtility.FromJson<SavedData>(savedString);

                savedData.count++;
            }
            else
            {
                savedData = new SavedData();
                savedData.name = "test";
                savedData.count = 0;
            }

            // Grab hero
            GameObject hero = GameManager.GetInstance().hero;
            if(hero)
            {
                // Grab Collector componet from the hero
                Collector collector = hero.GetComponent<Collector>();
                if(collector)
                {
                    // Create a list of save data items
                    // It is empty in the beginning and we will add items to it
                    List<SaveDataItem> items = new List<SaveDataItem>();

                    // Get number of items hero has
                    int itemCount = collector.collectedCollectables.Count;
                    for(int i = 0; i < itemCount; i++)
                    {
                        // Grab i'th collectable
                        Collectable collectable = collector.collectedCollectables[i];

                        // Create new SaveDataItem for it
                        SaveDataItem itemData = new SaveDataItem();
                        itemData.name = collectable.name;

                        // Add item data to the list
                        items.Add(itemData);
                    }

                    // Set items in the save data using items in our list
                    savedData.items = items.ToArray();
                }
            }

            string jsonString = JsonUtility.ToJson(savedData);
            PlayerPrefs.SetString(SaveDataKeys.SaveDataKey, jsonString);

            Debug.Log("Save data: " + PlayerPrefs.GetString(SaveDataKeys.SaveDataKey));
        }
    }
}
