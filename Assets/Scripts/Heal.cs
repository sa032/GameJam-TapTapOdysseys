using UnityEngine;

public class Heal : MonoBehaviour
{
    public MagicManager magicManager;
    public Health player;
    public void heal()
    {
        if (magicManager.currentMana < 50)
        {
            return;
        }
        magicManager.currentMana -= 50;
        player.health += player.maxHealth/4;
    }
}
