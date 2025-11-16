using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class MapLayer
    {
        public NodeType nodeType;
        [Tooltip("If Random = 0. 100% chance to get this node")]
        [Range(0f, 1f)] public float randomizeNodes;
        public float nodesApartDistance;
        public float distanceFromPreviousLayer;
    }
}