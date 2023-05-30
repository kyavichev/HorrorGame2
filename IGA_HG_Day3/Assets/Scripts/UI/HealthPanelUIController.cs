using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script updates UI to show current health state of a tracked destructible object
public class HealthPanelUIController : MonoBehaviour
{
    // Reference to the destructible for which health will be shown
    public Destructible destructible;

    // Reference to the prefab that contains visuals for each segment (portion) of the health bar / view
    public HealthBarSegmentView healthPanelSegmentPrefab;

    // List of all the health segments (in order from left to right)
    private List<HealthBarSegmentView> _segments;

    // Used to keep track of last shown information in order to reduce number of updates
    private int _lastShownHealthCount = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (destructible)
        {
            // Checks if health count has changed, if not - then return from the update function early
            if (destructible.GetCurrentHealthPoints() == _lastShownHealthCount)
            {
                return;
            }
            
            // Get current health point count
            int healthPoints = destructible.GetCurrentHealthPoints();
            
            // Loop through all visible segments which is determined by max health points
            for (int i = 0; i < destructible.maxHealthPoints; i++)
            {
                if (i < healthPoints)
                {
                    _segments[i].fullHealthView.SetActive(true);
                }
                else
                {
                    _segments[i].fullHealthView.SetActive(false);
                }
            }
        }
        else
        {
            // Set all segments as inactive
            for (int i = 0; i < _segments.Count; i++)
            {
                _segments[i].fullHealthView.SetActive(false);
            }
        }
    }


    // This functions sets up the panel for the current destructible
    private void Setup()
    {
        if (_segments == null)
        {
            _segments = new List<HealthBarSegmentView>();
        }
        
        if (destructible == null)
        {
            for (int i = 0; i < _segments.Count; i++)
            {
                _segments[i].gameObject.SetActive(false);
            }
        }
        else
        {
            int iterationCount = Mathf.Max(_segments.Count, destructible.maxHealthPoints);
            for (int i = 0; i < iterationCount; i++)
            {
                HealthBarSegmentView newSegment = Instantiate<HealthBarSegmentView>(healthPanelSegmentPrefab);
                newSegment.transform.SetParent(transform);
                newSegment.transform.localScale = Vector3.one;
                _segments.Add(newSegment);
            }
        }
    }
}
