using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Scorch", menuName = "Magic/Rare/Scorch")]
public class Scorch : MagicBase
{
    public GameObject MagicCard;
    public float damage;
    public override void Cast()
    {
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        List<GameObject> enemies = enemyManager.GetAllEnemies();
        AudioSource.PlayClipAtPoint(audioClip, enemyManager.transform.position);
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Instantiate(magicParticles, enemy.transform.position, Quaternion.identity);
                Effects enemyEffects = enemy.GetComponent<Effects>();
                if (enemyEffects.burning)
                {
                    Health health = enemy.GetComponent<Health>();

                    float damageBuffCalculate = damage;
                    Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
                    BuffContainData buff = BuffContainData.instance;
                    // its divided by 3 for flames
                    damageBuffCalculate = (damage+(buff.MagicBuffFlat/2))*(1+buff.MagicBuffPercent/100);
                    if (playerEffects.weak)
                    {
                        damageBuffCalculate *= 0.5f;
                    }

                    
                    health.damage(damageBuffCalculate * enemyEffects.burnRemaining);
                    enemyEffects.BurnStop();
                }
            }
        }
    }
}
