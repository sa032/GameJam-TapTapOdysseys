using System.Collections.Generic;
using OneLine;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        public List<NodeBlueprint> nodeBlueprints;
        [Tooltip("RandomNode that use to random")]
        public List<NodeType> randomNodes = new List<NodeType>
            {NodeType.EliteEnemy, NodeType.Shop, NodeType.Treasure, NodeType.MinorEnemy, NodeType.Rest};
        public int GridWidth => Mathf.Max(numOfPreBossNodes, numOfStartingNodes);

        [OneLineWithHeader]
        public int numOfPreBossNodes = 2;
        [OneLineWithHeader]
        public int numOfStartingNodes = 1;

        [Tooltip("generate paths")]
        public int extraPaths = 1;
        public List<MapLayer> layers;
    }
}