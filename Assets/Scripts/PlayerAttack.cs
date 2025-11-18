using UnityEngine;
using Unity.Cinemachine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    private EnemyManager enemyManager;

    public CinemachineImpulseSource impulse;
    private void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }
    public void Attack()
    {
        impulse.GenerateImpulse();
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
