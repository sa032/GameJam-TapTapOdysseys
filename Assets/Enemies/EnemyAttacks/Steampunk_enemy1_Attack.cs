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
        yield return StartCoroutine(BaseAttackCoroutine());
        enemyCooldown.attacking = false;
    }
    public IEnumerator BaseAttackCoroutine()
    {
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        Instantiate(warning, warningPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(SingleAttackCoroutine(false));
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(SingleAttackCoroutine(true));
        
    }
    private IEnumerator SingleAttackCoroutine(bool stunAfterParry)
    {
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
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
            player.damage(damage);
        }
    }
}
