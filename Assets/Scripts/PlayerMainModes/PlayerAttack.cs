using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    public UnityEvent nextAttack;
    private EnemyManager enemyManager;
    private Animator anim;
    public CinemachineImpulseSource impulse;
    public GameObject slash;
    public AudioSource audioSource;
    public MagicManager magicManager;
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
        audioSource.Play();

        if (enemy != null)
        {
            Health health = enemy.GetComponent<Health>();
            Effects effects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
            if (health != null)
            {
                float damageBuffCalculate = damage;

                BuffContainData buff = BuffContainData.instance;
                damageBuffCalculate = (damage+buff.DamageBuffFlat)*(1+buff.DamageBuffPercent/100);
                if (effects.weak)
                {
                    damageBuffCalculate *= 0.5f;
                }

                magicManager.currentMana += 5;
                health.damage(damageBuffCalculate);
                Instantiate(slash, enemy.transform.position, Quaternion.identity);
            }
        }
        if (GlobalValues.NextAttack)
        {
            nextAttack.Invoke();
            GlobalValues.NextAttack = false;
        }
        anim.Play("PlayerAttack");
        GlobalValues.barLocked = true;
        yield return new WaitForSeconds(0.2f);
        anim.Play("PlayerIdle");
        GlobalValues.barLocked = false;
    }
    public void SetNextAttack(UnityAction attack)
    {
        nextAttack.RemoveAllListeners();
        nextAttack.AddListener(attack);
    }
}
