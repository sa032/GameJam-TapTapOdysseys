using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public static MagicManager instance; 
    public MagicBase[] magicSlots = new MagicBase[3];
    void Start()
    {
        instance = this;
    }
    public void CastCurrentMagic(int slot)
    {
        if (magicSlots[slot] == null)
        {
            Debug.Log("No magic in slot!");
            return;
        }

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
