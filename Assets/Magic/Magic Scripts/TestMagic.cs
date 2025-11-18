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
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        GameObject enemy = enemyManager.GetFirstEnemy();
        if (enemy != null)
        {
            Effects enemyEffects = enemy.GetComponent<Effects>();
            enemyEffects.StartCoroutine(enemyEffects.Burn(duration, damage));
        }
    }
}