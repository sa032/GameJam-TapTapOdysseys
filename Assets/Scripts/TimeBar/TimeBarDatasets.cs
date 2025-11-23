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
    public TimedEventDataset[] datasets;
}