using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloorDataPool : ScriptableObject
{
    public List<Enemy> Enemy;
    public List<Enemy> EliteEnemy;
    public List<Enemy> Boss;
}
