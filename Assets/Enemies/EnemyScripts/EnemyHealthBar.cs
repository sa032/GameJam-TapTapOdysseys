using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Health enemy;       // reference to your script
    public Transform fill;          // the child spriteObject
    public float maxWidth;    // full bar height

    void Update()
    {
        float ratio = Mathf.Clamp01(enemy.health / enemy.maxHealth);

        // Because cooldown counts DOWN, flip ratio
        float barValue =ratio;

        // Scale only Y
        fill.localScale = new Vector3(
            barValue * maxWidth,
            fill.localScale.y,
            fill.localScale.z
        );
    }
}
