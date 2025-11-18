using UnityEngine;

public class EnemyCooldownBar : MonoBehaviour
{
    public EnemyCooldown enemy;       // reference to your script
    public Transform fill;          // the child spriteObject
    public float maxHeight = 1f;    // full bar height

    void Update()
    {
        float ratio = Mathf.Clamp01(enemy.currentCoolDown / enemy.coolDown);

        // Because cooldown counts DOWN, flip ratio
        float barValue =ratio;

        // Scale only Y
        fill.localScale = new Vector3(
            fill.localScale.x,
            barValue * maxHeight,
            fill.localScale.z
        );
    }
}
