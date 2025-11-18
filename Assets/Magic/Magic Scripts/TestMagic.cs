using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "TestMagic", menuName = "Magic/Test Magic")]
public class TestMagic : MagicBase
{
    public GameObject MagicCard;
    public int duration;
    public float damage;
    public override void Cast()
    {
        Effects enemyEffects = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Effects>();
        enemyEffects.StartCoroutine(enemyEffects.Burn(duration, damage));
        
    }
}