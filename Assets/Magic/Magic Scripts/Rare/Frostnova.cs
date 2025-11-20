using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Frostnova", menuName = "Magic/Rare/Frostnova")]
public class Frostnova : MagicBase
{
    public GameObject MagicCard;
    public int duration;
    public override void Cast()
    {
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        List<GameObject> enemies = enemyManager.GetAllEnemies();
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Effects enemyEffects = enemy.GetComponent<Effects>();
                if (enemyEffects.frozen)
                {
                    enemyEffects.WeakInflict(duration);
                    enemyEffects.FragileInflict(duration);
                    enemyEffects.FreezeStop();
                }
            }
        }
    }
}
