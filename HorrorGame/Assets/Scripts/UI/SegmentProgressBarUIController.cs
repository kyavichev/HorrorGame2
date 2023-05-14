using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentProgressBarUIController : MonoBehaviour
{
    public List<ProgressBarSegmentView> segments = new List<ProgressBarSegmentView>();
    public ProgressBarSegmentView segmentPrefab;
    public GameObject parentNode;

    public int segmentCount { get { return segments.Count; } }


    public void Generate(int count)
    {
        for(int i = 0; i < count; i++)
        {
            ProgressBarSegmentView newSegment = Instantiate< ProgressBarSegmentView>(segmentPrefab);
            newSegment.transform.SetParent(parentNode.transform, false);

            segments.Add(newSegment);
        }
    }


    public void UpdateToIndex(int lastActiveIndex)
    {
        if (lastActiveIndex < segments.Count)
        {
            for (int i = 0; i < segments.Count; i++)
            {
                ProgressBarSegmentView segment = segments[i];
                if(i <= lastActiveIndex)
                {
                    segment.activeView.SetActive(true);
                }
                else
                {
                    segment.activeView.SetActive(false);
                }
            }
        }
    }
}
