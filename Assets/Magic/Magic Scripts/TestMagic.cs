using UnityEngine;

[CreateAssetMenu(fileName = "TestMagic", menuName = "Magic/Test Magic")]
public class TestMagic : MagicBase
{
    public GameObject MagicCard;
    public override void Cast()
    {
        Debug.Log("Casting projectile magic!");
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        player.damage(3);
    }
}