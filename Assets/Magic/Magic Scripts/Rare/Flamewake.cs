using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Flamewake", menuName = "Magic/Rare/Flamewake")]
public class Flamewake : MagicBase
{
    public GameObject MagicCard;
    public int duration;
    public float damage;
    public override void Cast()
    {
        bool oneBurning = false;

        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        List<GameObject> enemies = enemyManager.GetAllEnemies();
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Effects enemyEffects = enemy.GetComponent<Effects>();
                if (enemyEffects.burning)
                {
                    oneBurning = true;
                    break;
                }
            }
        }
        if (oneBurning)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    Effects enemyEffects = enemy.GetComponent<Effects>();
                    enemyEffects.BurnInflict(duration, damage);
                    oneBurning = false;
                }
            }
        }
    }
}
