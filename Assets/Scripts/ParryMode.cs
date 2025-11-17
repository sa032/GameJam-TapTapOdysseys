using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class ParryMode : MonoBehaviour
{
    public bool inParryMode;
    public GameObject parryEffect;
    public float parryFrame;
    private void Update()
    {
        if (!inParryMode)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("parry");
            StartCoroutine(startParrying());
        }
        if (GlobalValues.parried)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Instantiate(parryEffect, player.position, quaternion.identity);
            GlobalValues.parrying = false;
            GlobalValues.parried = false;
            StopAllCoroutines();
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
        yield return new WaitForSeconds(parryFrame);
        GlobalValues.parrying = false;
        exitParryMode();
    }
}
