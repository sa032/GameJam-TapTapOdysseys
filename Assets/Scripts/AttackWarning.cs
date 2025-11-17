using UnityEngine;

public class AttackWarning : MonoBehaviour
{
    public float lerpSpeed;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.position + Vector3.up;
        Destroy(this.gameObject, 2);
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
