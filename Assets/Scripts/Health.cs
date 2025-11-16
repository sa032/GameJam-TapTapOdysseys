using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    public void damage(float damageAmount)
    {
        health -= damageAmount;
    }
    public void heal(float healAmount)
    {
        health += healAmount;
    }
}
