using UnityEngine;
using System.Collections;

public class Boss2_Attack2 : MonoBehaviour
{
    public float damage;
    public GameObject warning;
    public Transform warningPos;
    public Transform parentCooldown;
    public GameObject particle;
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
        Instantiate(particle, player.transform.position, Quaternion.identity);
        if (GlobalValues.parrying)
        {
            GlobalValues.parried = true;
            yield return new WaitForSeconds(5);
        }
        else
        {
            playerEffects.FragileInflict(7);
        }
    }
}
