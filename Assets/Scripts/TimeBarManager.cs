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
    private RectTransform[] markerRects;
    private Image[] markerImages;

    private void Start()
    {
        
        currentValue = minValue;
        TimeBar = GetComponent<Slider>();
        setMarkers(true);
    }
    private void Update()
    {
        currentValue += Time.deltaTime * sliderSpeed;
        setTimeBarValues();
        setMarkers(false);
        
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
    private void setMarkers(bool spawnMarks)
    {
        RectTransform markerRect = markerParent.GetComponent<RectTransform>();
        float sliderWidth = markerRect.rect.width;
        float fullRange = maxValue - minValue;

        if (spawnMarks)
        {
            markerRects = new RectTransform[events.Length];
            markerImages = new Image[events.Length];
        }

        for (int i = 0; i < events.Length; i++)
        {
            var ev = events[i];

            RectTransform r;
            Image img;

            if (spawnMarks)
            {
                GameObject m = Instantiate(markerPrefab, markerParent);
                r = m.GetComponent<RectTransform>();
                img = m.GetComponent<Image>();

                markerRects[i] = r;
                markerImages[i] = img;
            }
            else
            {
                r = markerRects[i];
                img = markerImages[i];
            }

            img.color = ev.color;

            float startNorm = Mathf.InverseLerp(minValue, maxValue, ev.minTime);
            float endNorm   = Mathf.InverseLerp(minValue, maxValue, ev.maxTime);

            float width = (endNorm - startNorm) * sliderWidth;
            float xStart = Mathf.Lerp(-sliderWidth / 2f, sliderWidth / 2f, startNorm);
            float xPos = xStart + width / 2f;

            r.sizeDelta = new Vector2(width, r.sizeDelta.y);
            r.anchoredPosition = new Vector2(xPos, 0);
        }
    }
}
