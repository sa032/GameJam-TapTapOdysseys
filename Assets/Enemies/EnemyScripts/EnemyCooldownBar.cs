using UnityEngine;

public class EnemyCooldownBar : MonoBehaviour
{
    public EnemyCooldown enemy;       // reference to your script
    private Material instanceMaterial;

    private void Start()
    {
        // This creates a unique copy for THIS object only
        instanceMaterial = Instantiate(GetComponent<SpriteRenderer>().material);
        GetComponent<SpriteRenderer>().material = instanceMaterial;
    }

    void Update()
    {
        float ratio = Mathf.Clamp01(enemy.currentCoolDown / enemy.coolDown);

        // Because cooldown counts DOWN, flip ratio
        float angle = ratio * 360f;

        instanceMaterial.SetFloat("_Arc1", 360 - angle);
    }
}
