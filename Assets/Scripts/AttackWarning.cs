using UnityEngine;

public class AttackWarning : MonoBehaviour
{
    public float lerpSpeed;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.position + Vector3.up;   // y + 1
    }

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * lerpSpeed
        );
    }
}
