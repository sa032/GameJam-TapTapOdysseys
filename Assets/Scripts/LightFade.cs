using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFade : MonoBehaviour
{
    private Light2D thislight;
    private void Start()
    {
        thislight = GetComponent<Light2D>();
    }
    private void FixedUpdate()
    {
        thislight.intensity = Mathf.MoveTowards(thislight.intensity, 0, 0.3f);
    }
}
