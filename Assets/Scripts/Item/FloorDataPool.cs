using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloorDataPool : ScriptableObject
{
    public List<GameObject> EnemyPrefab;
    public List<GameObject> EliteEnemyPrefab;
    public List<GameObject> BossPrefab;
    public List<Encounter> encounters;
}
