using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private Vector3 launch;
    private TextMeshPro tmp;
    public float damageNumber;
    private void Start()
    {
        transform.localPosition = transform.localPosition + (Vector3)Random.insideUnitCircle * 1f;
        launch = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        tmp = GetComponent<TextMeshPro>();
        tmp.text = damageNumber.ToString();
        float ClampX = Mathf.Clamp(0.1f*damageNumber,3,15);
        float ClampY = Mathf.Clamp(0.1f*damageNumber,3,15);
        transform.localScale = new Vector3(ClampX, ClampY, 1);

        Destroy(gameObject, 5);
    }
    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale, 
            Vector3.zero, 
            6 * Time.deltaTime
        );

        transform.localPosition += launch * 6 * Time.deltaTime;
    }
}
