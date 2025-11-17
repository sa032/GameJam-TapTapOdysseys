using UnityEngine;

public class BuffContainData : MonoBehaviour
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

    public static BuffContainData instance;
    void Start()
    {
        instance = this;
    }
}
