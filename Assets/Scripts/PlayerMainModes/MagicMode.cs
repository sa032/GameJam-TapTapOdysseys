using System.Collections;
using UnityEngine;

public class MagicMode : MonoBehaviour
{
    private Animator anim;
    private TimeBarManager timeBarManager;
    public bool inMagicMode;
    private MagicManager magicManager;
    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        timeBarManager = GameObject.FindGameObjectWithTag("TimeBarManager").GetComponent<TimeBarManager>();
        magicManager = FindAnyObjectByType<MagicManager>();
    }
    private void Update()
    {
        if (!inMagicMode)
        {
            return;
        }
    }
    public void enterMagicMode()
    {
        inMagicMode = true;
        timeBarManager.SwitchDataset(2);
    }
    public void exitMagicMode()
    {
        inMagicMode = false;
        timeBarManager.SwitchDataset(1);
    }
    public void CastMagic(int slot)
    {
        StartCoroutine(CastMagicAnimation());
        magicManager.CastCurrentMagic(slot);
    }
    private IEnumerator CastMagicAnimation()
    {
        anim.Play("PlayerMagic");
        GlobalValues.barLocked = true;
        yield return new WaitForSeconds(0.3f);
        anim.Play("PlayerIdle");
        GlobalValues.barLocked = false;
    }
}
