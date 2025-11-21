using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Flux", menuName = "Magic/Rare/Flux")]
public class Flux : MagicBase
{
    public GameObject MagicCard;
    public float timeAdd;
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
                if (enemyEffects.burning) enemyEffects.burnRemaining += timeAdd;
                if (enemyEffects.cold) enemyEffects.coldRemaining += timeAdd;
                if (enemyEffects.frozen) enemyEffects.freezeRemaining += timeAdd;
                if (enemyEffects.fragile) enemyEffects.fragileRemaining += timeAdd;
                if (enemyEffects.weak) enemyEffects.weakRemaining += timeAdd;
            }
        }
    }
}
