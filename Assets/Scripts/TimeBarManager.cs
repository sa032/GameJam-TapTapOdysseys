using UnityEngine;
using UnityEngine.Events;

public struct TimedEvent
{
    public float minTime;
    public float maxTime;
    public UnityEvent eventTime;
}

public class TimeBarManager : MonoBehaviour
{
    private float minValue = -10;
    private float maxValue = 10;
    private float currentValue;

    public TimedEvent[] events;

    private void Start()
    {
        currentValue = minValue;
    }
    private void Update()
    {
        currentValue += Time.deltaTime;
        foreach (var timedEvent in events)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentValue < timedEvent.maxTime && currentValue > timedEvent.minTime)
                {
                    timedEvent.eventTime.Invoke();
                }
            }
        }
    }
}
