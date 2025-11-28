using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashUI : MonoBehaviour
{
    public Color flashColor = Color.red;      // สีที่จะกระพริบไป
    public float flashSpeed = 2f;             // ความเร็วในการเฟดสี

    private Image[] images;                   // เก็บทุก Image ที่เป็นลูก

    void Start()
    {
        // ดึงทุก Image ที่เป็น children ทั้งหมด
        images = GetComponentsInChildren<Image>();

        // เก็บ original color ของแต่ละตัวไว้ก่อน
        foreach (var img in images)
        {
            // เพื่อให้แต่ละ img มี original ของตัวเอง
            img.gameObject.AddComponent<FlashData>().originalColor = img.color;
        }

        StartCoroutine(FlashLoop());
    }

    IEnumerator FlashLoop()
    {
        while (true)   // วนลูปไม่รู้จบ
        {
            // เฟดจาก original → flashColor
            yield return StartCoroutine(LerpColorAll(0f, 1f));

            // เฟดจาก flashColor → original
            yield return StartCoroutine(LerpColorAll(1f, 0f));
        }
    }

    IEnumerator LerpColorAll(float start, float end)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * flashSpeed;

            foreach (var img in images)
            {
                var data = img.GetComponent<FlashData>();
                img.color = Color.Lerp(data.originalColor, flashColor, Mathf.Lerp(start, end, t));
            }

            yield return null;
        }
    }

    // ใช้เก็บ Original Color ของแต่ละ Image
    private class FlashData : MonoBehaviour
    {
        public Color originalColor;
    }
}
