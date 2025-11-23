using UnityEngine;

[CreateAssetMenu(fileName = "Weaken", menuName = "Magic/Common/Weaken")]
public class Weaken : MagicBase
{
    public GameObject MagicCard;
    public int duration;
    public override void Cast()
    {
        EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        GameObject enemy = enemyManager.GetFirstEnemy();
        if (enemy != null)
        {
            Instantiate(magicParticles, enemy.transform.position, Quaternion.identity);
            Effects enemyEffects = enemy.GetComponent<Effects>();
            enemyEffects.WeakInflict(duration);
            AudioSource.PlayClipAtPoint(audioClip, enemy.transform.position);
        }
    }
}
