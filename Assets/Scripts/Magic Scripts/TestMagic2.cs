using UnityEngine;

[CreateAssetMenu(fileName = "TestMagic2", menuName = "Magic/Test Magic2")]
public class TestMagic2 : MagicBase
{
    public GameObject MagicCard;
    public override void Cast()
    {
        Debug.Log("Casting bruh");
        Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        player.damage(3);
    }
}
