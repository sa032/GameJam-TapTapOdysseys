using UnityEngine;

public class fade : MonoBehaviour
{
    public float duration = 2f;   // effect duration
    private float timer;
    private SpriteRenderer sr;
    private Vector3 startScale;
    public Vector3 targetScale = new Vector3(2f, 2f, 2f); // desired size

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / duration;

        // Increase size
        transform.localScale = Vector3.Lerp(startScale, targetScale, t);

        // Decrease opacity
        Color c = sr.color;
        c.a = Mathf.Lerp(1f, 0f, t);
        sr.color = c;
    }
}
