using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Scorch", menuName = "Magic/Rare/Scorch")]
public class Scorch : MagicBase
{
    public GameObject MagicCard;
    public float baseDamage;
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
                    health.damage(baseDamage * enemyEffects.burnRemaining);
                    enemyEffects.BurnStop();
                }
            }
        }
    }
}
