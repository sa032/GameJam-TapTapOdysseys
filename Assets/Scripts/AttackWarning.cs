using UnityEngine;
using System.Collections;

public class AttackWarning : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 target;
    public float duration = 1f;
    public IEnumerator MoveAnimated()
    {
    Vector3 startPos = origin;
    Vector3 endPos = target;
    float t = 0;

    while (t < duration)
    {
        t += Time.deltaTime;
        float p = t / duration;

        // Smoothstep animation
        float curve = Mathf.SmoothStep(0, 1, p);

        transform.position = Vector3.Lerp(startPos, endPos, curve);
        yield return null;
    }
    }
}
