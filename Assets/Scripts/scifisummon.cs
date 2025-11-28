using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scifisummon : MonoBehaviour
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
        animator.Play("ScifiAttack");
        SingleAttack(false);
        yield return new WaitForSeconds(0.2f);
        animator.Play("ScifiIdle");
        yield return new WaitForSeconds(0.2f);
        animator.Play("ScifiAttack");
        SingleAttack(true);
        yield return new WaitForSeconds(0.2f);
        animator.Play("ScifiIdle");
    }
    private void SingleAttack(bool burn)
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        List<GameObject> enemies = enemyManager.GetAllEnemies();
        foreach (GameObject enemy in enemies)
        {
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
                        effects.FragileInflict(3);
                    }
                }
            }
        }  
    }
}
