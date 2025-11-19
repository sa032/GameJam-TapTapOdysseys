using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    private EnemyManager enemyManager;
    private Animator anim;
    public CinemachineImpulseSource impulse;
    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }
    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    private IEnumerator AttackCoroutine()
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
        anim.Play("PlayerAttack");
        GlobalValues.barLocked = true;
        yield return new WaitForSeconds(0.3f);
        anim.Play("PlayerIdle");
        GlobalValues.barLocked = false;
    }
}
