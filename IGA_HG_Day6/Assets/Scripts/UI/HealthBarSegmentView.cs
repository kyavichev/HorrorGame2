using UnityEngine;

// Component that keeps track of 2 game objects that represent one segment of the heath panel
public class HealthBarSegmentView : MonoBehaviour
{
    // Reference to the game object that shows filled heart (red heart)
    public GameObject fullHealthView;
    // Reference to the game object that show empty heart (grey heart)
    public GameObject emptyHealthView;
}
