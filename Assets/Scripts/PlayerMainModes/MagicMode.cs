using System.Collections;
using UnityEngine;

public class MagicMode : MonoBehaviour
{
    private Animator anim;
    private TimeBarManager timeBarManager;
    private MagicManager magicManager;
    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        timeBarManager = GameObject.FindGameObjectWithTag("TimeBarManager").GetComponent<TimeBarManager>();
        magicManager = FindAnyObjectByType<MagicManager>();
    }
    public void enterMagicMode()
    {
        timeBarManager.SwitchDataset(1);
    }
    public void exitMagicMode()
    {
        timeBarManager.SwitchDataset(0);
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
