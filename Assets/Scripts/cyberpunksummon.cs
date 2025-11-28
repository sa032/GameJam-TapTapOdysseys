using UnityEngine;
using System.Collections;

public class cyberpunksummon : MonoBehaviour
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
        animator.Play("CyberpunkAttack");
        SingleAttack(false);
        yield return new WaitForSeconds(0.3f);
        animator.Play("CyberpunkIdle");
        yield return new WaitForSeconds(0.3f);
        animator.Play("CyberpunkAttack");
        SingleAttack(true);
        yield return new WaitForSeconds(0.3f);
        animator.Play("CyberpunkIdle");
    }
    private void SingleAttack(bool burn)
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        GameObject enemy = enemyManager.GetFirstEnemy();

        if (enemy != null)
        {
            Health health = enemy.GetComponent<Health>();
            Effects effects = enemy.GetComponent<Effects>();
            if (health != null)
            {
                float damageBuffCalculate = damage;

                LevelManager level = LevelManager.instance;
                damageBuffCalculate = damage * level.currentLevel;

                health.damage(damageBuffCalculate);
                if (burn)
                {
                    effects.BurnInflict(3, damageBuffCalculate*0.2f);
                }
            }
        }
    }
}
