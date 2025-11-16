using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void Attack()
    {
        EnemyHealth enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.damageEnemy(3);
        }
    }
}
