using UnityEngine;

public class GetMagicMode : MonoBehaviour
{
    public MagicBase selectedMagic;
    public MagicBase[] magicSlots = new MagicBase[3];
    public void SelectMagic(int slot)
    {
        if (magicSlots[slot] != null)
        {
            
            TimeBarManager timeBarManager = GameObject.FindGameObjectWithTag("TimeBarManager").GetComponent<TimeBarManager>();
            selectedMagic = magicSlots[slot];
            timeBarManager.SwitchDataset(4);
        }
    }
    public void GetMagic(int slot)
    {
        MagicManager magicManager = GetComponent<MagicManager>();
        if (selectedMagic != null)
        {
            TimeBarManager timeBarManager = GameObject.FindGameObjectWithTag("TimeBarManager").GetComponent<TimeBarManager>();
            magicManager.SetMagic(slot, selectedMagic);
            timeBarManager.SwitchDataset(1);
        }
    }
}
