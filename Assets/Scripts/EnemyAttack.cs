using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float coolDown;
    private float currentCoolDown;
    private void Update()
    {
        currentCoolDown -= Time.deltaTime;
        if (currentCoolDown < 0)
        {
            Attack();
        }
    }
    public void Attack()
    {
        Health health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        if (health != null)
        {
            health.damage(3);
        }
        currentCoolDown = coolDown;
    }
}