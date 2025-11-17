using System;
using UnityEngine;

namespace Map
{
    public enum NodeType
    {
        Enemy,
        EliteEnemy,
        Rest,
        Treasure,
        Shop,
        Boss,
        Encounter
    }
}

namespace Map
{
    [CreateAssetMenu]
    public class NodeBlueprint : ScriptableObject
    {
        public Sprite sprite;
        public NodeType nodeType;
        [TextArea]
        public string description;
    }
}