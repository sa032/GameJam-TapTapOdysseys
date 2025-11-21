using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Blizzard", menuName = "Magic/Rare/Blizzard")]
public class Blizzard : MagicBase
{
    public GameObject MagicCard;
    public int duration;
    public void blizzardAttack()
    {
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        List<GameObject> enemies = enemyManager.GetAllEnemies();
        AudioSource.PlayClipAtPoint(audioClip, enemyManager.transform.position);
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Effects enemyEffects = enemy.GetComponent<Effects>();
                enemyEffects.ColdInflict(duration);
            }
        }
    }
    public override void Cast()
    {
        PlayerAttack playerAttack = GameObject.FindGameObjectWithTag("TimeBarDatasets").GetComponent<PlayerAttack>();
        playerAttack.SetNextAttack(blizzardAttack);
        GlobalValues.NextAttack = true;
    }
}
