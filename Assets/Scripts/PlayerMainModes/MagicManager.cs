using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public float currentMana;
    public static MagicManager instance; 
    public float maxMana;
    public MagicBase[] magicSlots = new MagicBase[3];
    void Start()
    {
        instance = this;
    }
    private void Update()
    {
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
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
