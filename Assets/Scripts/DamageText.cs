using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TextMeshPro tmp;
    public float damageNumber;
    public float lerpSpeed;
    private void Start()
    {
        transform.position = Random.insideUnitCircle * 0.2f;
    }
    private void Update()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0);
        transform.position = Vector3.Lerp(
            transform.position,
            transform.position + Vector3.up,
            Time.deltaTime * lerpSpeed
        );
    }
}
