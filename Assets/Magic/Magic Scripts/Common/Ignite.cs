using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Ignite", menuName = "Magic/Common/Ignite")]
public class Ignite : MagicBase
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
            Instantiate(magicParticles, enemy.transform.position, Quaternion.identity);
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
            AudioSource.PlayClipAtPoint(audioClip, enemy.transform.position);
        }
    }
}