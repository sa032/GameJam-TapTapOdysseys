using UnityEngine;

public class BuffContainData : MonoBehaviour
{
    [Header("Damage Buff")]
    public float DamageBuffPercent;
    public float DamageBuffFlat;
    [Header("Magic Buff")]
    public float MagicBuffFlat;
    public float MagicBuffPercent;
    [Header("Defense Buff")]
    public float DefenseBuffPercent;
    public float DefenseBuffFlat;
    [Header("HP Buff")]
    public float HPBuffPercent;
    public float HPBuffFlat;
    [Header("Time")]
    public bool IsStartTimer;
    public float PlayTime;
    [Header("Kill")]
    public int Kill;

    public static BuffContainData instance;
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        if (IsStartTimer)
        {
            PlayTime += Time.deltaTime;
        }
    }
}
