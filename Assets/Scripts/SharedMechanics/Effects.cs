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
    public void BurnInflict(int duration, float damage)
    {
        if (burning)
        {
            StopCoroutine(burnCoroutine);
        }
        burnCoroutine = StartCoroutine(Burn(duration, damage));
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
    public void ColdInflict(float duration)
    {
        if (frozen)
        {
            StopCoroutine(freezeCoroutine);
            freezeCoroutine = StartCoroutine(Freeze(duration));
            return;
        }
        if (cold)
        {
            StopCoroutine(coldCoroutine);
            cold = false;
            freezeCoroutine = StartCoroutine(Freeze(duration));
            return;
        }
        coldCoroutine = StartCoroutine(Cold(duration));
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
    public void FragileInflict(float duration)
    {
        if (fragile)
        {
            StopCoroutine(fragileCoroutine);
        }
        fragileCoroutine = StartCoroutine(Fragile(duration));
    }
    public IEnumerator Fragile(float duration)
    {
        fragile = true;
        yield return new WaitForSeconds(duration);
        fragile = false;
    }
    public void WeakInflict(float duration)
    {
        if (weak)
        {
            StopCoroutine(weakCoroutine);
        }
        weakCoroutine = StartCoroutine(Weak(duration));
    }
    public IEnumerator Weak(float duration)
    {
        weak = true;
        yield return new WaitForSeconds(duration);
        weak = false;
    }
}
