using UnityEngine;

// This component spawns specified amount of GameObjects based on the specified prefab
public class SimpleSpamSpawner : MonoBehaviour
{
    // Reference to the prefab to use as a blueprint
    public GameObject prefabToSpawn;
    // Reference to the game object that will be the parent for all spawned objects
    public GameObject objectParent;

    // Number of objects to spawn
    public int numToSpawn = 10;
    // Number of objects already spawned
    private int _numSpawned = 0;
    
    // How often an object should be spawned
    public float frequency = 3;
    // Timer to keep track of time between spawns
    private float _timer = 0;



    // Update is called once per frame
    void Update()
    {
        // Check if enough objects were spawned already
        if (_numSpawned < numToSpawn)
        {
            // Update timer variable with time that has passed between frames
            _timer += Time.deltaTime;
            
            // If timer is larger than frequency - time to spawn a new object
            if (_timer > frequency)
            {
                // Reset timeer
                _timer = 0;
                // Increment spawned count by 1
                _numSpawned = _numSpawned + 1;

                // Instantiate a new game object based on the prefab
                GameObject newObject = Instantiate(prefabToSpawn);
                // Set the parent of the newly created object to the specified parent
                newObject.transform.SetParent(objectParent.transform);
                // Reset position to zero (within parent's space)
                newObject.transform.localPosition = Vector3.zero;
                // Reset scale to 1 (within parent's space)
                newObject.transform.localScale = Vector3.one;
            }
        }
    }
}
