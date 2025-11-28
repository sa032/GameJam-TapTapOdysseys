using UnityEngine;
using System.Collections;

public class y2ksummon : MonoBehaviour
{
    private EnemyManager enemyManager;
    public float damage;
    public Animator animator;
    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    public IEnumerator AttackCoroutine()
    {
        animator.Play("Y2KAttack");
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        GameObject enemy = enemyManager.GetFirstEnemy();

        if (enemy != null)
        {
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                float damageBuffCalculate = damage;

                LevelManager level = LevelManager.instance;
                damageBuffCalculate = damage * level.currentLevel;

                health.damage(damageBuffCalculate);
            }
        }
        yield return new WaitForSeconds(0.5f);
        animator.Play("Y2KIdle");
    }
}
