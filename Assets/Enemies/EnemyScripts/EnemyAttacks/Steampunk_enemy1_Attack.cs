using UnityEngine;
using System.Collections;

public class Steampunk_enemy1_Attack : MonoBehaviour
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
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        // do
        
    }
    private IEnumerator singleAttack(bool stunAfterAttack)
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        if (GlobalValues.parrying)
        {
            GlobalValues.parried = true;
            if (stunAfterAttack)
            {
                yield return new WaitForSeconds(5);
            }
        }
        else
        {
            player.damage(damage);
        }
    }
}
