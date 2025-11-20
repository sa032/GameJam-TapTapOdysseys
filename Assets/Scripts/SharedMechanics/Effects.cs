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
    public float burnRemaining;
    public float coldRemaining;
    public float freezeRemaining;
    public float fragileRemaining;
    public float weakRemaining;
    public ParticleSystem burnParticles;
    public ParticleSystem coldParticles;
    public ParticleSystem freezeParticles;
    public ParticleSystem fragileParticles;
    public ParticleSystem weakParticles;
    public void Start()
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
        if (burning)
        {
            //fix
            burnParticles.Play();
        }
        else
        {
            
        }
        if (cold)
        {
            
        }
        if (frozen)
        {
            
        }
        if (fragile)
        {
            
        }
        if (weak)
        {
            
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
    public IEnumerator Burn(float duration, float damage)
    {
        burning = true;
        burnRemaining = duration;
        while (burnRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            health.damage(damage);
            burnRemaining -= 1;
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
        coldRemaining = duration;
        while (coldRemaining > 0)
        {
            yield return new WaitForSeconds(duration);
            coldRemaining -= 1;
        }
        cold = false;
    }
    public IEnumerator Freeze(float duration)
    {
        frozen = true;
        freezeRemaining = duration;
        while (freezeRemaining > 0)
        {
            yield return new WaitForSeconds(duration);
            freezeRemaining -= 1;
        }
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
        fragileRemaining = duration;
        while (fragileRemaining > 0)
        {
            yield return new WaitForSeconds(duration);
            fragileRemaining -= 1;
        }
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
        weakRemaining = duration;
        while (weakRemaining > 0)
        {
            yield return new WaitForSeconds(duration);
            weakRemaining -= 1;
        }
        weak = false;
    }
}
