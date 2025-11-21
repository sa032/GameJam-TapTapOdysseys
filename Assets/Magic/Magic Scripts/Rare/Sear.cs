using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Sear", menuName = "Magic/Rare/Sear")]
public class Sear : MagicBase
{
    public GameObject MagicCard;
    public int duration;
    public override void Cast()
    {
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        List<GameObject> enemies = enemyManager.GetAllEnemies();
        AudioSource.PlayClipAtPoint(audioClip, enemyManager.transform.position);
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Effects enemyEffects = enemy.GetComponent<Effects>();
                if (enemyEffects.burning)
                {
                    enemyEffects.FragileInflict(duration);
                }
            }
        }
    }
}
