using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void Attack()
    {
        Health health = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
        if (health != null)
        {
            health.damage(3);
        }
    }
}
