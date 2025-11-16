using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class MapLayer
    {
        public NodeType nodeType;
        [Tooltip("If Random = 0. 100% chance to get this node")]
        [Range(0f, 1f)] public float randomizeNodes;
        [Tooltip("If this is set to 0, nodes on this layer will appear in a straight line. Closer to 1f = more position randomization")]
        public float nodesApartDistance;
        
        public float distanceFromPreviousLayer;
    }
}