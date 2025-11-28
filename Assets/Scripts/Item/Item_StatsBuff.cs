using UnityEngine;

public class Item_StatsBuff : MonoBehaviour
{
    [Header("Damage Buff")]
    public float DamageBuffPercent;
    public float DamageBuffFlat;
    [Header("Defense Buff")]
    public float DefenseBuffPercent;
    public float DefenseBuffFlat;
    [Header("HP Buff")]
    public float HPBuffPercent;
    public float HPBuffFlat;
    [Header("Magic Buff")]
    public float MagicBuffPercent;
    public float MagicBuffFlat;
    void Awake()
    {
        BuffContainData.instance.DamageBuffPercent += DamageBuffPercent;
        BuffContainData.instance.DamageBuffFlat += DamageBuffFlat;

        BuffContainData.instance.DefenseBuffPercent += DefenseBuffPercent;
        BuffContainData.instance.DefenseBuffFlat += DefenseBuffFlat;

        BuffContainData.instance.HPBuffPercent += HPBuffPercent;
        BuffContainData.instance.HPBuffFlat += HPBuffFlat;

        BuffContainData.instance.MagicBuffPercent += MagicBuffPercent;
        BuffContainData.instance.MagicBuffFlat += MagicBuffFlat;

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            Player.GetComponent<Health>().health += HPBuffFlat;
        }
    }

    void Update()
    {
        
    }
    public void RemoveThisItem()
    {
        BuffContainData.instance.DamageBuffPercent -= DamageBuffPercent;
        BuffContainData.instance.DamageBuffFlat -= DamageBuffFlat;

        BuffContainData.instance.DefenseBuffPercent -= DefenseBuffPercent;
        BuffContainData.instance.DefenseBuffFlat -= DefenseBuffFlat;

        BuffContainData.instance.HPBuffPercent -= HPBuffPercent;
        BuffContainData.instance.HPBuffFlat -= HPBuffFlat;

        Destroy(this.gameObject);
    }
}
