using UnityEngine;
[CreateAssetMenu(fileName = "FlameBurst", menuName = "Magic/Common/FlameBurst")]
public class FlameBurst : MagicBase
{
    public GameObject MagicCard;
    public float damage;
    public override void Cast()
    {
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        GameObject enemy = enemyManager.GetFirstEnemy();
        if (enemy != null)
        {
            Effects enemyEffects = enemy.GetComponent<Effects>();
            Instantiate(magicParticles, enemy.transform.position, Quaternion.identity);
            if (enemyEffects.burning)
            {
                Health health = enemy.GetComponent<Health>();

                float damageBuffCalculate = damage;
                Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
                BuffContainData buff = BuffContainData.instance;
                // its divided by 3 for flames
                damageBuffCalculate = ((damage+buff.MagicBuffFlat)*2.5f)*(1+buff.MagicBuffPercent/100);
                if (playerEffects.weak)
                {
                    damageBuffCalculate *= 0.5f;
                }

                health.damage(damageBuffCalculate);
                AudioSource.PlayClipAtPoint(audioClip, enemy.transform.position);
            }
            else
            {
                Health health = enemy.GetComponent<Health>();

                float damageBuffCalculate = damage;
                Effects playerEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<Effects>();
                BuffContainData buff = BuffContainData.instance;
                // its divided by 3 for flames
                damageBuffCalculate = (damage+buff.MagicBuffFlat)*(1+buff.MagicBuffPercent/100);
                if (playerEffects.weak)
                {
                    damageBuffCalculate *= 0.5f;
                }

                health.damage(damageBuffCalculate);
                AudioSource.PlayClipAtPoint(audioClip, enemy.transform.position);
            }
        }
    }
}
