using UnityEngine;
using System.Collections;

public class FinalBoss_Attack2 : MonoBehaviour
{
    public float damage;
    public float burnDamage;
    public GameObject warning;
    public Transform warningPos;
    public Transform parentCooldown;
    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    public IEnumerator AttackCoroutine()
    {
        EnemyCooldown enemyCooldown = parentCooldown.GetComponent<EnemyCooldown>();
        enemyCooldown.attacking = true;
        yield return StartCoroutine(SingleAttackCoroutine());
        enemyCooldown.attacking = false;
    }
    public IEnumerator SingleAttackCoroutine()
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
        if (GlobalValues.parrying)
        {
            GlobalValues.parried = true;
            yield return new WaitForSeconds(10);
        }
        else
        {
            playerEffects.FragileInflict(5);
            playerEffects.BurnInflict(5,burnDamage);
            playerEffects.ColdInflict(5);
            playerEffects.WeakInflict(5);
        }
    }
}
