using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Data;
using System;
[System.Serializable]
public struct TimedEvent
{
    public string Name;
    public bool isMagic;
    public int MagicIndex;
    public float minTime;
    public float maxTime;
    public UnityEvent eventTime;
    public Color color; 
}
public class TimeBarManager : MonoBehaviour
{
    private Slider TimeBar;
    public static TimeBarManager instance;

    [Header("Slider Settings")]
    public float minValue = -10;
    public float maxValue = 10;
    public float currentValue;
    public float sliderSpeed;
    public float sliderMultiplier;

    [Header("Timed Events")]
    public TimeBarDatasets currentDataset;
    public TimedEvent[] events;
    
    [Header("Marker Settings")]
    public RectTransform markerParent;
    public GameObject markerPrefab;
    public RectTransform handle;
    private RectTransform[] markerRects;
    private Image[] markerImages;
    private Effects effects;

    private void Start()
    {
        instance = this;
        effects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
        currentValue = minValue;
        TimeBar = GetComponent<Slider>();
        SwitchDataset(0);
    }
    public Animator Spacebar;
    private void Update()
    {
        if (GlobalValues.barLocked)
        {
            return;
        }
        if (effects != null)
        {
            if (effects.frozen)
            {
                sliderMultiplier = 0.05f;
            }else if (effects.cold)
            {
                sliderMultiplier = 0.5f;
            }
            else
            {
                sliderMultiplier = 1;
            }
        }
        setTimeBarValues();
        setMarkers(false);
        if (Input.GetKey(KeyCode.Space))
        {
            Spacebar.SetBool("IsPress",true);
            currentValue += Time.deltaTime * sliderSpeed * sliderMultiplier;
            if (currentValue >= maxValue)
            {
                StartCoroutine(resetCurrentValue());
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Spacebar.SetBool("IsPress",false);
            bool matched = false;
            foreach (var timedEvent in events)
            {
                if (currentValue < timedEvent.maxTime && currentValue > timedEvent.minTime)
                {
                    timedEvent.eventTime.Invoke();
                    matched = true;
                    break; // stop checking after success
                }
            }
            if (!matched)
            {
                StartCoroutine(LockForTime(50));
            }
            StartCoroutine(resetCurrentValue());
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
            if(ev.minTime - ev.maxTime != 0){
                if (spawnMarks)
                {
                    GameObject m = Instantiate(markerPrefab, markerParent);
                    r = m.GetComponent<RectTransform>();
                    img = m.GetComponent<Image>();
                    TextMeshProUGUI text = m.transform.Find("Title").Find("TextTitle").GetComponent<TextMeshProUGUI>();
                    MagicBase magicBase = MagicManager.instance.magicSlots[events[i].MagicIndex];
                    
                    if(events[i].isMagic == true && magicBase != null)
                    {
                        text.text = magicBase.Name;
                    }else
                    text.text = events[i].Name;
                    

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
    private IEnumerator resetCurrentValue()
    {
        yield return null;
        currentValue = 0;
    }
    private IEnumerator LockForTime(int times)
    {
        Vector2 OrigPos = handle.anchoredPosition;
        GlobalValues.barLocked = true;
        for (int i = 0; i < times; i++)
        {
            handle.anchoredPosition = OrigPos + UnityEngine.Random.insideUnitCircle * 10f;
            yield return new WaitForSeconds(0.02f);
        }
        handle.anchoredPosition = OrigPos;
        GlobalValues.barLocked = false;
    }
    private void ClearMarkers()
    {
        if (markerRects != null)
        {
            foreach (var r in markerRects)
            {
                if (r != null)
                    Destroy(r.gameObject);
            }
        }
    }
    public void SwitchDataset(int index)
    {
        ClearMarkers();
        events = currentDataset.datasets[index].elements;
        setMarkers(true);
    }
}
