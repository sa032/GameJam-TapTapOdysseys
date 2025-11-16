using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    public void Attack()
    {
        Health health = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
        if (health != null)
        {
            health.damage(damage);
        }
    }
}
