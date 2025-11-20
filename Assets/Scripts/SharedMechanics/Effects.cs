using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour
{
    private Health health;
    public bool burning;
    public bool cold;
    public bool frozen;
    public bool fragile;
    public bool weak;
    private Coroutine burnCoroutine;
    private Coroutine coldCoroutine;
    private Coroutine freezeCoroutine;
    private Coroutine fragileCoroutine;
    private Coroutine weakCoroutine;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    private void Update()
    {
        if (burning && frozen)
        {
            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
            if (freezeCoroutine != null)
            {
                StopCoroutine(freezeCoroutine);
            }
            burning = false;
            frozen = false;
            Shock();
        }
    }
    public void Shock()
    {
        health.damage(7);
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
        if (frozen)
        {
            StopCoroutine(freezeCoroutine);
            freezeCoroutine = StartCoroutine(Freeze(3));
            return;
        }
        if (cold)
        {
            StopCoroutine(coldCoroutine);
            cold = false;
            freezeCoroutine = StartCoroutine(Freeze(3));
            return;
        }
        coldCoroutine = StartCoroutine(Cold(3));
    }
    public IEnumerator Cold(float duration)
    {
        cold = true;
        yield return new WaitForSeconds(duration);
        cold = false;
    }
    public IEnumerator Freeze(float duration)
    {
        frozen = true;
        yield return new WaitForSeconds(duration);
        frozen = false;
    }
    public void fragileInflict()
    {
        if (fragile)
        {
            StopCoroutine(fragileCoroutine);
        }
        fragileCoroutine = StartCoroutine(Fragile(3));
    }
    public IEnumerator Fragile(float duration)
    {
        fragile = true;
        yield return new WaitForSeconds(duration);
        fragile = false;
    }
    public void weakInflict()
    {
        if (weak)
        {
            StopCoroutine(weakCoroutine);
        }
        weakCoroutine = StartCoroutine(Weak(3));
    }
    public IEnumerator Weak(float duration)
    {
        weak = true;
        yield return new WaitForSeconds(duration);
        weak = false;
    }
}
