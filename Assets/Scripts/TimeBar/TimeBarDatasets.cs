using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeBarDatasets : MonoBehaviour
{
    [System.Serializable]
    public class TimedEventDataset
    {
        public TimedEvent[] elements;
    }
    public List<TimedEventDataset> datasets;
    public TimedEvent[] dataset1;
    public TimedEvent[] dataset2;
    public TimedEvent[] dataset3;
    public TimedEvent[] dataset4;
}