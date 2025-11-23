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
            enemyEffects.BurnInflict(duration, damage);
            AudioSource.PlayClipAtPoint(audioClip, enemy.transform.position);
        }
    }
}