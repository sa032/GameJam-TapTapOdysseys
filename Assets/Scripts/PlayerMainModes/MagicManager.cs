using UnityEditor.Scripting;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public float currentMana;
    public MagicBase[] magicSlots = new MagicBase[3];
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TimeBarManager timeBarManager = GameObject.FindGameObjectWithTag("TimeBarManager").GetComponent<TimeBarManager>();
            timeBarManager.SwitchDataset(2);
        }
        
    }
    public void CastCurrentMagic(int slot)
    {
        if (magicSlots[slot] == null)
        {
            Debug.Log("No magic in slot!");
            return;
        }
        if (magicSlots[slot].manaCost > currentMana)
        {
            Debug.Log("Not Enough Mana!");
            return;
        }
        currentMana -= magicSlots[slot].manaCost;
        magicSlots[slot].Cast();
    }

    public void SetMagic(int slot, MagicBase newMagic)
    {
        magicSlots[slot] = newMagic;
    }

    public void ClearMagic(int slot)
    {
        magicSlots[slot] = null;
    }
}
