using UnityEngine;
using System.Collections;

public class Boss2_Attack : MonoBehaviour
{
    public float damage1;
    public float damage2;
    public float damage3;
    public GameObject warning;
    public Transform warningPos;
    public Transform parentCooldown;
    public GameObject slash;
    public GameObject particle;
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
                enemyCooldown.coolDown = 4;
                enemyCooldown.currentCoolDown = 4;
                break;

            case 3:
                yield return StartCoroutine(BaseAttackCoroutine3());
                break;

            default:
                Debug.Log("bruh");
                break;
        }
        index = Random.Range(1,4);
        if (index == 2)
        {
            enemyCooldown.coolDown = 1.5f;
            enemyCooldown.currentCoolDown = 1.5f;
        }
        enemyCooldown.attacking = false;
    }
    public IEnumerator BaseAttackCoroutine1()
    {
        animator.Play("SamuraiBossWindup");
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        animator.Play("SamuraiBossAttack1");
        StartCoroutine(SingleAttackCoroutine1(false));
        yield return new WaitForSeconds(0.1f);
        animator.Play("SamuraiBossWindup");
        yield return new WaitForSeconds(0.2f);
        animator.Play("SamuraiBossAttack1");
        StartCoroutine(SingleAttackCoroutine1(false));
        yield return new WaitForSeconds(0.1f);
        animator.Play("SamuraiBossWindup");
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        animator.Play("SamuraiBossAttack1");
        StartCoroutine(SingleAttackCoroutine1(false));
        yield return new WaitForSeconds(0.1f);
        animator.Play("SamuraiBossWindup");
        yield return new WaitForSeconds(0.2f);
        animator.Play("SamuraiBossAttack1");
        yield return StartCoroutine(SingleAttackCoroutine1(true));
        yield return new WaitForSeconds(0.2f);
        animator.Play("SamuraiBossIdle");
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
        animator.Play("SamuraiBossWindup");
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        animator.Play("SamuraiBossAttack2");
        yield return StartCoroutine(SingleAttackCoroutine2(true));
        yield return new WaitForSeconds(0.2f);
        animator.Play("SamuraiBossIdle");
    }
    private IEnumerator SingleAttackCoroutine2(bool stunAfterParry)
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
        Instantiate(particle, player.transform.position, Quaternion.identity);
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
            playerEffects.WeakInflict(5);
        }
    }
    public IEnumerator BaseAttackCoroutine3()
    {
        animator.Play("SamuraiBossWindup");
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(SingleAttackCoroutine3(false));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(SingleAttackCoroutine3(false));
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(SingleAttackCoroutine3(true));
        yield return new WaitForSeconds(0.2f);
        animator.Play("SamuraiBossIdle");
    }
    private IEnumerator SingleAttackCoroutine3(bool stunAfterParry)
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
        Instantiate(slash, player.transform.position, Quaternion.Euler(0, 180, 0));
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
            playerEffects.ColdInflict(3);
        }
    }
}
