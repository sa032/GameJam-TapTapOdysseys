using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinalSummon : MonoBehaviour
{
    public float damage1;
    public float damage2;
    private EnemyManager enemyManager;
    private int index = 1;
    public void Attack()
    {
        AttackCoroutine();
    }
    public void AttackCoroutine()
    {
        switch (index)
        {
            case 1:
                StartCoroutine(BaseAttackCoroutine1());
                break;

            case 2:
                StartCoroutine(BaseAttackCoroutine2());
                break;

            default:
                break;
        }
        index = Random.Range(1,3);
    } 
    public IEnumerator BaseAttackCoroutine1()
    {
        SingleAttack1();
        yield return new WaitForSeconds(0.3f);
    }
    private void SingleAttack1()
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
                    float damageBuffCalculate = damage1;

                    LevelManager level = LevelManager.instance;
                    damageBuffCalculate = damage1 * level.currentLevel;

                    health.damage(damageBuffCalculate);

                    effects.BurnInflict(5, damageBuffCalculate * 0.3f);

                }
            }
        }
    }
    public IEnumerator BaseAttackCoroutine2()
    {
        SingleAttack2();
        yield return new WaitForSeconds(0.3f);
    }
    private void SingleAttack2()
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
                    float damageBuffCalculate = damage2;

                    LevelManager level = LevelManager.instance;
                    damageBuffCalculate = damage2 * level.currentLevel;

                    health.damage(damageBuffCalculate);

                    effects.ColdInflict(1.5f);

                }
            }
        }
    }
}
