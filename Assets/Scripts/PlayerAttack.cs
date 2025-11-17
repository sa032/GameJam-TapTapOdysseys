using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    private EnemyManager enemyManager;
    private void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }
    public void Attack()
    {
        GameObject enemy = enemyManager.GetFirstEnemy();

        if (enemy != null)
        {
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                health.damage(damage);
            }
        }
    }
}
