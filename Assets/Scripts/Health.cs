using UnityEngine;
using System.Collections;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void damage(float damageAmount)
    {
        health -= damageAmount;
        StartCoroutine(Shake(5));
        Debug.Log(damageAmount);
    }
    public void heal(float healAmount)
    {
        health += healAmount;
        Debug.Log(healAmount);
    }
    private IEnumerator Shake(int times)
    {
        Vector3 OrigPos = transform.position;
        for (int i = 0; i < times; i++)
        {
            transform.position = OrigPos + (Vector3)(Random.insideUnitCircle * 0.2f);
            yield return new WaitForSeconds(0.02f);
        }
        transform.position = OrigPos;
    }
}
