using UnityEngine;
using System.Collections;

public class Boss1_Attack : MonoBehaviour
{
    public float damage1;
    public float damage2;
    public float damage3;
    public float burnDamage;
    public GameObject warning;
    public Transform warningPos;
    public Transform parentCooldown;
    public GameObject Attack2Effect;
    public GameObject Attack3Effect;
    public Animator animator;
    private int index = 1;
    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    public IEnumerator AttackCoroutine()
    {
        EnemyCooldown enemyCooldown = parentCooldown.GetComponent<EnemyCooldown>();
        enemyCooldown.attacking = true;
        switch (index)
        {
            case 1:
                yield return StartCoroutine(BaseAttackCoroutine1());
                break;

            case 2:
                yield return StartCoroutine(BaseAttackCoroutine2());
                break;

            case 3:
                yield return StartCoroutine(BaseAttackCoroutine3());
                break;

            default:
                Debug.Log("bruh");
                break;
        }
        
        index = Random.Range(1,4);
        enemyCooldown.attacking = false;
    }
    public IEnumerator BaseAttackCoroutine1()
    {
        animator.Play("DesertBossWindup");
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        animator.Play("DesertBossAttack1");
        StartCoroutine(SingleAttackCoroutine1(false));
        yield return new WaitForSeconds(0.1f);
        animator.Play("DesertBossWindup");
        yield return new WaitForSeconds(0.2f);
        animator.Play("DesertBossAttack1");
        StartCoroutine(SingleAttackCoroutine1(false));
        yield return new WaitForSeconds(0.1f);
        animator.Play("DesertBossWindup");
        yield return new WaitForSeconds(0.2f);
        animator.Play("DesertBossAttack1");
        yield return StartCoroutine(SingleAttackCoroutine1(true));
        yield return new WaitForSeconds(0.2f);
        animator.Play("DesertBossIdle");
    }
    private IEnumerator SingleAttackCoroutine1(bool stunAfterParry)
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
        if (GlobalValues.parrying)
        {
            GlobalValues.parried = true;
            if (stunAfterParry)
            {
                yield return new WaitForSeconds(5);
            }
        }
        else
        {
            player.damage(damage1);
        }
    }
    public IEnumerator BaseAttackCoroutine2()
    {
        animator.Play("DesertBossWindup");
        yield return new WaitForSeconds(1f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        animator.Play("DesertBossAttack2");
        StartCoroutine(SingleAttackCoroutine2(false, true));
        yield return new WaitForSeconds(0.1f);
        animator.Play("DesertBossWindup");
        yield return new WaitForSeconds(0.2f);
        animator.Play("DesertBossAttack2");
        yield return StartCoroutine(SingleAttackCoroutine2(true, false));
        yield return new WaitForSeconds(0.2f);
        animator.Play("DesertBossIdle");
    }
    private IEnumerator SingleAttackCoroutine2(bool stunAfterParry, bool fragile)
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
        if (fragile)
        {
            Instantiate(Attack2Effect, player.transform.position, Quaternion.identity);
        }
        if (GlobalValues.parrying)
        {
            GlobalValues.parried = true;
            if (stunAfterParry)
            {
                yield return new WaitForSeconds(5);
            }
        }
        else
        {
            player.damage(damage2);
            if (fragile)
            {
                playerEffects.FragileInflict(6);
            }
        }
    }
    public IEnumerator BaseAttackCoroutine3()
    {
        animator.Play("DesertBossWindup");
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        animator.Play("DesertBossAttack3");
        yield return StartCoroutine(SingleAttackCoroutine3(true));
        yield return new WaitForSeconds(0.2f);
        animator.Play("DesertBossIdle");
    }
    private IEnumerator SingleAttackCoroutine3(bool stunAfterParry)
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
        Instantiate(Attack3Effect, player.transform.position, Quaternion.identity);
        if (GlobalValues.parrying)
        {
            GlobalValues.parried = true;
            if (stunAfterParry)
            {
                yield return new WaitForSeconds(5);
            }
        }
        else
        {
            player.damage(damage3);
            playerEffects.BurnInflict(5,burnDamage);
        }
    }
}
