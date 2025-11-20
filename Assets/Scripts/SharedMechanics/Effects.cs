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
    [HideInInspector]public Coroutine burnCoroutine;
    [HideInInspector]public Coroutine coldCoroutine;
    [HideInInspector]public Coroutine freezeCoroutine;
    [HideInInspector]public Coroutine fragileCoroutine;
    [HideInInspector]public Coroutine weakCoroutine;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    private void Update()
    {
        if (burning && frozen)
        {
            BurnStop();
            FreezeStop();
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
    public void BurnStop()
    {
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
            burnCoroutine = null;
        }
        burning = false;
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
    public void ColdStop()
    {
        if (coldCoroutine != null)
        {
            StopCoroutine(coldCoroutine);
            coldCoroutine = null;
        }
        cold = false;
    }
    public void FreezeStop()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
            freezeCoroutine = null;
        }
        frozen = false;
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
    public void FragileStop()
    {
        if (fragileCoroutine != null)
        {
            StopCoroutine(fragileCoroutine);
            fragileCoroutine = null;
        }
        fragile = false;
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
    public void WeakStop()
    {
        if (weakCoroutine != null)
        {
            StopCoroutine(weakCoroutine);
            weakCoroutine = null;
        }
        weak = false;
    }
    public IEnumerator Weak(float duration)
    {
        weak = true;
        yield return new WaitForSeconds(duration);
        weak = false;
    }
}
