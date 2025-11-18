using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFade : MonoBehaviour
{
    private Light2D thislight;
    public float fadeSpeed;
    private void Start()
    {
        thislight = GetComponent<Light2D>();
    }
    private void FixedUpdate()
    {
        thislight.intensity = Mathf.MoveTowards(thislight.intensity, 0, fadeSpeed);
    }
}
