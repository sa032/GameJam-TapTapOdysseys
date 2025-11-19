using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour
{
    private Health health;
    public bool burning;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    public IEnumerator Burn(int duration, float damage)
    {
        burning = true;
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1);
            health.damage(damage);
        }
        burning = false;
    }
}
