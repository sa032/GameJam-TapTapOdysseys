using System.Collections.Generic;
using OneLine;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        public List<NodeBlueprint> nodeBlueprints;
        [Tooltip("Nodes that will be used on layers with Randomize Nodes > 0")]
        public List<NodeType> randomNodes = new List<NodeType>
            {NodeType.Encounter, NodeType.Shop, NodeType.Treasure, NodeType.Enemy, NodeType.Rest};
        public int GridWidth => Mathf.Max(numOfPreBossNodes.max, numOfStartingNodes.max);

        [OneLineWithHeader]
        public IntMinMax numOfPreBossNodes;
        [OneLineWithHeader]
        public IntMinMax numOfStartingNodes;

        [Tooltip("Increase this number to generate more paths")]
        public int extraPaths;
        //public List<MapLayer> layers;
        public List<Floor> FloorLayers;
    }

    [System.Serializable]
    public class Floor
    {
        public string Name;
        public Sprite MapImage;
        public List<MapLayer> layers;
    }
}