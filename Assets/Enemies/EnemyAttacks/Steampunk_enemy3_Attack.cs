using UnityEngine;
using System.Collections;

public class Steampunk_enemy3_Attack : MonoBehaviour
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
        yield return StartCoroutine(BaseAttackCoroutine());
        enemyCooldown.attacking = false;
    }
    public IEnumerator BaseAttackCoroutine()
    {
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        animator.Play("DesertSamuraiAttack");
        yield return StartCoroutine(SingleAttackCoroutine(true));
        yield return new WaitForSeconds(0.2f);
        animator.Play("DesertSamuraiIdle");
        
    }
    private IEnumerator SingleAttackCoroutine(bool stunAfterParry)
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
        if (GlobalValues.parrying)
        {
            GlobalValues.parried = true;
            if (stunAfterParry)
            {
                yield return new WaitForSeconds(3);
            }
        }
        else
        {
            player.damage(damage);
            playerEffects.WeakInflict(5);
            playerEffects.FragileInflict(5);
        }
    }
}
