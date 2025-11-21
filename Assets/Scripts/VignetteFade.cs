using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteFade : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;
    private void Start()
    {
        volume = GetComponent<Volume>();
        if (volume.profile.TryGet<Vignette>(out Vignette v))
        {
            vignette = v;
        }
    }
    private void Update()
    {
        if (vignette != null)
        {
            vignette.intensity.value -= Time.deltaTime * 0.7f;
            vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, 0, 1);
        }
    }
}
