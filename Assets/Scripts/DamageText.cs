using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private Vector3 launch;
    private TextMeshPro tmp;
    public float damageNumber;
    public float lerpSpeed;
    private void Start()
    {
        transform.localPosition = (Vector3)Random.insideUnitCircle * 1f;
        launch = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
    }
    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale, 
            Vector3.zero, 
            3 * Time.deltaTime
        );

        transform.localPosition += launch * 1 * Time.deltaTime;
    }
}
