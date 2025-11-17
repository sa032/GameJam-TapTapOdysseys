using UnityEngine;
using System.Collections;

public class AttackTest : MonoBehaviour
{
    public float damage;
    public GameObject warning;
    public Transform warningPos;
    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    public IEnumerator AttackCoroutine()
    {
        EnemyCooldown enemyCooldown = GetComponent<EnemyCooldown>();
        enemyCooldown.attacking = true;
        yield return StartCoroutine(SingleAttackCoroutine());
        enemyCooldown.attacking = false;
    }
    public IEnumerator SingleAttackCoroutine()
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        if (GlobalValues.parrying)
        {
            
        }
        else
        {
            player.damage(damage);
        }
    }
}
