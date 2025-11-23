using UnityEngine;
using System.Collections;

public class Y2K_enemy2_Attack : MonoBehaviour
{
    public float damage;
    public GameObject warning;
    public Transform warningPos;
    public Transform parentCooldown;
    public Animator animator;
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
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        animator.Play("Y2KSilAttack");
        if (GlobalValues.parrying)
        {
            GlobalValues.parried = true;
            yield return new WaitForSeconds(5);
        }
        else
        {
            player.damage(damage);
            playerEffects.BurnInflict(3, damage * 0.2f);
        }
        yield return new WaitForSeconds(0.2f);
        animator.Play("Y2KSilIdle");
    }
}
