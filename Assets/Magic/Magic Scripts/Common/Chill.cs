using UnityEngine;

[CreateAssetMenu(fileName = "Chill", menuName = "Magic/Common/Chill")]
public class Chill : MagicBase
{
    public GameObject MagicCard;
    public int duration;
    public override void Cast()
    {
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        GameObject enemy = enemyManager.GetFirstEnemy();
        if (enemy != null)
        {
            Effects enemyEffects = enemy.GetComponent<Effects>();
            enemyEffects.ColdInflict(duration);
        }
    }
}
