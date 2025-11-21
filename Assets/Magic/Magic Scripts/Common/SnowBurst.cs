using UnityEngine;

[CreateAssetMenu(fileName = "SnowBurst", menuName = "Magic/Common/SnowBurst")]
public class SnowBurst : MagicBase
{
    public GameObject MagicCard;
    public float damage;
    public float burstDamage;
    public override void Cast()
    {
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        GameObject enemy = enemyManager.GetFirstEnemy();
        if (enemy != null)
        {
            Effects enemyEffects = enemy.GetComponent<Effects>();
            if (enemyEffects.cold)
            {
                Health health = enemy.GetComponent<Health>();
                health.damage(burstDamage);
                AudioSource.PlayClipAtPoint(audioClip, enemy.transform.position);
            }
            else
            {
                Health health = enemy.GetComponent<Health>();
                health.damage(damage);
                AudioSource.PlayClipAtPoint(audioClip, enemy.transform.position);
            }
        }
    }
}
