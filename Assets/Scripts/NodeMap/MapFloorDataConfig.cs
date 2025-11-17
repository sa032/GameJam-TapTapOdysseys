using System.Collections.Generic;
using OneLine;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu]
    public class MapFloorDataConfig : ScriptableObject
    {    
        public List<FloorDataPool> FloorData;
    }
    [System.Serializable]
    public class FloorDataPool
    {
        public string Name;
        public List<Enemy> Enemy;
        public List<Enemy> EliteEnemy;
        public List<Enemy> Boss;
    }
    [System.Serializable]
    public class Enemy : ScriptableObject
    {
        public GameObject prefab;
        [Header("Stats")]
        public int HP;
        public int Damage;
    }
}