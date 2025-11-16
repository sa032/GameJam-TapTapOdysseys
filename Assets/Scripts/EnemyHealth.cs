using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public TMP_Text healthText;
    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        
        if (healthText != null)
            healthText.text = $"{health} / {maxHealth}";
    }
    public void damageEnemy(float damageAmount)
    {
        health -= damageAmount;
    }
    public void healEnemy(float healAmount)
    {
        health += healAmount;
    }
}
