using UnityEngine;
using System.Collections;

public class FinalBoss_Attack : MonoBehaviour
{
    public float damage1;
    public float damage2;
    public float damage3;
    public float damage4;
    public GameObject warning;
    public Transform warningPos;
    public Transform parentCooldown;
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

            case 4:
                yield return StartCoroutine(BaseAttackCoroutine4());
                enemyCooldown.coolDown = 4;
                enemyCooldown.currentCoolDown = 4;
                break;
            default:
                Debug.Log("bruh");
                break;
        }
        index = Random.Range(1,5);
        if (index == 2)
        {
            enemyCooldown.coolDown = 1;
            enemyCooldown.currentCoolDown = 1;
        }
        if (index == 4)
        {
            enemyCooldown.coolDown = 3;
            enemyCooldown.currentCoolDown = 3;
        }
        enemyCooldown.attacking = false;
    }
    public IEnumerator BaseAttackCoroutine1()
    {
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(SingleAttackCoroutine1(false));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SingleAttackCoroutine1(false));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SingleAttackCoroutine1(false));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SingleAttackCoroutine1(false));
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(SingleAttackCoroutine1(true));
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
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(SingleAttackCoroutine2(true));
    }
    private IEnumerator SingleAttackCoroutine2(bool stunAfterParry)
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
            player.damage(damage2);
            playerEffects.ColdInflict(3);
            playerEffects.WeakInflict(3);
        }
    }
    public IEnumerator BaseAttackCoroutine3()
    {
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(SingleAttackCoroutine3(false, false));
        yield return new WaitForSeconds(0.2f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(SingleAttackCoroutine3(false, false));
        yield return new WaitForSeconds(0.2f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(SingleAttackCoroutine3(false, false));
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(SingleAttackCoroutine3(true, true));
    }
    private IEnumerator SingleAttackCoroutine3(bool stunAfterParry, bool burn)
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
            player.damage(damage3);
            if (burn)
            {
                playerEffects.BurnInflict(5, 1);
            }
        }
    }
    public IEnumerator BaseAttackCoroutine4()
    {
        yield return new WaitForSeconds(2);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(SingleAttackCoroutine4(true));
    }
    private IEnumerator SingleAttackCoroutine4(bool stunAfterParry)
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
            player.damage(damage4);
        }
    }
}
