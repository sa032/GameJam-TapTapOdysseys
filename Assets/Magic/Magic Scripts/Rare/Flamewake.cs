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
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Instantiate(magicParticles, player.transform.position, Quaternion.identity);
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
            AudioSource.PlayClipAtPoint(audioClip, enemyManager.transform.position);
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    Effects enemyEffects = enemy.GetComponent<Effects>();

                    float damageBuffCalculate = damage;
                    Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
                    BuffContainData buff = BuffContainData.instance;
                    // its divided by 3 for flames
                    damageBuffCalculate = (damage+(buff.MagicBuffFlat/3))*(1+buff.MagicBuffPercent/100);
                    if (playerEffects.weak)
                    {
                        damageBuffCalculate *= 0.5f;
                    }

                    enemyEffects.BurnInflict(duration, damageBuffCalculate);
                    oneBurning = false;
                }
            }
        }
    }
}
