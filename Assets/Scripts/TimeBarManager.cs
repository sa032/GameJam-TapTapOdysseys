using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[System.Serializable]
public struct TimedEvent
{
    public float minTime;
    public float maxTime;
    public UnityEvent eventTime;
    public Color color; 
}

public class TimeBarManager : MonoBehaviour
{
    private Slider TimeBar;

    [Header("Slider Settings")]
    public float minValue = -10;
    public float maxValue = 10;
    public float currentValue;
    public float sliderSpeed;

    [Header("Timed Events")]
    public TimedEvent[] events;

    [Header("Marker Settings")]
    public RectTransform markerParent;
    public GameObject markerPrefab;

    private void Start()
    {
        
        currentValue = minValue;
        TimeBar = GetComponent<Slider>();
        setMarkers();
    }
    private void Update()
    {
        currentValue += Time.deltaTime * sliderSpeed;
        setTimeBarValues();
        
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
    private void setTimeBarValues()
    {
        TimeBar.maxValue = maxValue;
        TimeBar.minValue = minValue;
        TimeBar.value = currentValue;
    }
    private void setMarkers()
    {
        RectTransform markerRect = markerParent.GetComponent<RectTransform>();
        float sliderWidth = markerRect.rect.width;  
        float fullRange = maxValue - minValue;

        foreach (var ev in events)
        {
            GameObject marker = Instantiate(markerPrefab, markerParent);
        Image img = marker.GetComponent<Image>();
        RectTransform r = marker.GetComponent<RectTransform>();

        img.color = ev.color;

        // Normalize 0 → 1
        float startNorm = Mathf.InverseLerp(minValue, maxValue, ev.minTime);
        float endNorm   = Mathf.InverseLerp(minValue, maxValue, ev.maxTime);

        float width = (endNorm - startNorm) * sliderWidth;

        // Convert startNorm into a centered position
        float xStart = Mathf.Lerp(-sliderWidth / 2f, sliderWidth / 2f, startNorm);

        float xPos = xStart + width / 2f;

        // Apply the values
        r.sizeDelta = new Vector2(width, r.sizeDelta.y);
        r.anchoredPosition = new Vector2(xPos, 0);
        }
    }
}
