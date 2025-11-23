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
            if (health != null)
            {
                BuffContainData buff = BuffContainData.instance;
                float damageBuffCalculate = damage;
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
