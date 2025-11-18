using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using Unity.Cinemachine;

public class ParryMode : MonoBehaviour
{
    private Animator anim;
    private Coroutine parryCoroutine;
    public bool inParryMode;
    public GameObject parryEffect;
    public float parryFrame;
    public Transform parryPos;
    public CinemachineImpulseSource impulse;
    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
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
            GlobalValues.parrying = false;
            GlobalValues.parried = false;
            if (parryCoroutine != null)
            {
                StopCoroutine(parryCoroutine);
            }
            StartCoroutine(AfterParry());
        }

    }
    public void enterParryMode()
    {
        GlobalValues.barLocked = true;
        inParryMode = true;
    }
    private void exitParryMode()
    {
        GlobalValues.barLocked = false;
        inParryMode = false;
    }
    private IEnumerator startParrying()
    {
        GlobalValues.parrying = true;
        anim.Play("PlayerParry");
        yield return new WaitForSeconds(parryFrame);
        GlobalValues.parrying = false;
        anim.Play("PlayerIdle");
        exitParryMode();
    }
    private IEnumerator AfterParry()
    {
        yield return new WaitForSeconds(0.2f);
        anim.Play("PlayerIdle");
    }
}
