using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Break", menuName = "Magic/Common/Break")]
public class Break : MagicBase
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
            enemyEffects.FragileInflict(duration);
            AudioSource.PlayClipAtPoint(audioClip, enemy.transform.position);
        }
    }
}
