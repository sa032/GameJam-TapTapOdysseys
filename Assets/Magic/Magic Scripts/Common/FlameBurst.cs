using UnityEngine;
[CreateAssetMenu(fileName = "FlameBurst", menuName = "Magic/Common/FlameBurst")]
public class FlameBurst : MagicBase
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
            Instantiate(magicParticles, enemy.transform.position, Quaternion.identity);
            if (enemyEffects.burning)
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
