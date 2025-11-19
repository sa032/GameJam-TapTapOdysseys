using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour
{
    private Health health;
    public bool burning;
    public bool cold;
    private Coroutine burnCoroutine;
    private Coroutine coldCoroutine;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    public void BurnInflict()
    {
        if (burning)
        {
            StopCoroutine(burnCoroutine);
        }
        burnCoroutine = StartCoroutine(Burn(3, 1));
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
    public void ColdInflict()
    {
        if (cold)
        {
            StopCoroutine(coldCoroutine);
        }
        coldCoroutine = StartCoroutine(Cold(3));
    }
    public IEnumerator Cold(float duration)
    {
        cold = true;
        yield return new WaitForSeconds(duration);
        cold = false;
    }
}
