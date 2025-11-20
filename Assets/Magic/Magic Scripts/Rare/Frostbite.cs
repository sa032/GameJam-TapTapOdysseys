using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Frostbite", menuName = "Magic/Rare/Frostbite")]
public class Frostbite : MagicBase
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
                if (enemyEffects.cold)
                {
                    enemyEffects.WeakInflict(duration);
                }
            }
        }
    }
}
