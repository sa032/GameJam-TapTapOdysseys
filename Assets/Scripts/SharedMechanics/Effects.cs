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
    public GameObject freezeSprite;
    public GameObject fragileSprite;
    public ParticleSystem weakParticles;

    public void Start()
    {
        health = GetComponent<Health>();
        burnParticles = transform.Find("Particles/BurnEffect").GetComponent<ParticleSystem>();
        coldParticles = transform.Find("Particles/ColdEffect").GetComponent<ParticleSystem>();
        freezeSprite = transform.Find("Particles/FreezeEffect").gameObject;
        fragileSprite = transform.Find("Particles/FragileEffect").gameObject;
        weakParticles = transform.Find("Particles/WeakEffect").GetComponent<ParticleSystem>();
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
            if (!burnParticles.isPlaying)
                burnParticles.Play();
        }
        else
        {
            if (burnParticles.isPlaying)
                burnParticles.Stop();
        }
        if (cold)
        {
            if (!coldParticles.isPlaying)
                coldParticles.Play();
        }
        else
        {
            if (coldParticles.isPlaying)
                coldParticles.Stop();
        }
        if (frozen)
        {
            if (!freezeSprite.activeSelf)
                freezeSprite.SetActive(true);
        }
        else
        {
            if (freezeSprite.activeSelf)
                freezeSprite.SetActive(false);
        }
        if (fragile)
        {
            if (!fragileSprite.activeSelf)
                fragileSprite.SetActive(true);
        }
        else
        {
            if (fragileSprite.activeSelf)
                fragileSprite.SetActive(false);
        }
        if (weak)
        {
            if (!weakParticles.isPlaying)
                weakParticles.Play();
        }
        else
        {
            if (weakParticles.isPlaying)
                weakParticles.Stop();
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
            burnRemaining = 0;
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
            coldRemaining = 0;
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
            coldRemaining = 0;
            coldCoroutine = null;
        }
        cold = false;
    }
    public void FreezeStop()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
            freezeRemaining = 0;
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
            yield return new WaitForSeconds(1);
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
            yield return new WaitForSeconds(1);
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
            fragileRemaining = 0;
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
            yield return new WaitForSeconds(1);
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
            weakRemaining = 0;
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
            yield return new WaitForSeconds(1);
            weakRemaining -= 1;
        }
        weak = false;
    }
}
