using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ParryMode : MonoBehaviour
{
    private Animator anim;
    private Coroutine parryCoroutine;
    public bool inParryMode;
    public GameObject parryEffect;
    public float parryFrame;
    public Transform parryPos;
    public CinemachineImpulseSource impulse;
    public AudioSource SFX;
    public MagicManager magicManager;

    public Volume volume;
    private Vignette vignette;
    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        
        if (volume.profile.TryGet<Vignette>(out Vignette v))
        {
            vignette = v;
        }
    }
    private void Update()
    {
        if (!inParryMode)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("parry");
            parryCoroutine = StartCoroutine(startParrying());
        }
        if (GlobalValues.parried)
        {
            impulse.GenerateImpulse();
            Instantiate(parryEffect, parryPos.position, Quaternion.identity);
            vignette.intensity.value = 0.5f;
            GlobalValues.parrying = false;
            GlobalValues.parried = false;
            magicManager.currentMana += 20;
            SFX.Play();
            if (parryCoroutine != null)
            {
                StopCoroutine(parryCoroutine);
            }
            anim.Play("PlayerParry");
        }

    }
    public void enterParryMode()
    {
        GlobalValues.barLocked = true;
        GlobalValues.blocking = true;
        anim.Play("PlayerParry");
        inParryMode = true;
    }
    private void exitParryMode()
    {
        GlobalValues.barLocked = false;
        GlobalValues.blocking = false;
        anim.Play("PlayerIdle");
        inParryMode = false;
    }
    private IEnumerator startParrying()
    {
        GlobalValues.parrying = true;
        anim.Play("PlayerAttack");
        yield return new WaitForSeconds(parryFrame);
        GlobalValues.parrying = false;
        exitParryMode();
    }
}
