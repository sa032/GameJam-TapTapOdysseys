using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour
{
    private Health health;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    public IEnumerator Burn(int duration, float damage)
    {
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1);
            health.damage(damage);
        }
    }
}
