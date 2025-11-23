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
            Instantiate(magicParticles, enemy.transform.position, Quaternion.identity);
            enemyEffects.ColdInflict(duration);
            AudioSource.PlayClipAtPoint(audioClip, enemy.transform.position);
        }
    }
}
