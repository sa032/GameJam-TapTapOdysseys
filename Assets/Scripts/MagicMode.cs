using UnityEngine;
using UnityEngine.Events;

public class MagicMode : MonoBehaviour
{
    private TimeBarManager timeBarManager;
    public bool inMagicMode;
    private MagicManager magicManager;
    private void Start()
    {
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
        magicManager.CastCurrentMagic(slot);
    }
}
