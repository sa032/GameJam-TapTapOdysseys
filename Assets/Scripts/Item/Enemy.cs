using UnityEngine;

[System.Serializable]
    [CreateAssetMenu]
    public class Enemy : ScriptableObject
    {
        public GameObject prefab;
        [Header("Stats")]
        public int HP;
        public int Damage;
    }