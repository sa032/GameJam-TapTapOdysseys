using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapNodeSelectUI : MonoBehaviour
    {
        public MapManager mapManager;
        
        List<Node> GetNextNode()
        {
            List<Node> a = null;
            
            if(mapManager.CurrentMap.path.Count == 0)
            {
                a.Add(mapManager.CurrentMap.GetNode(new Vector2Int(0,0)));
                return a;
            }else{
                Vector2Int currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);
                foreach(Vector2Int n in currentNode.outgoing)
                {
                    print(n.x+" "+n.y);
                    
                }
                return a;
            }
        }
        
        public void GetNextNodeUI()
        {
            List<Node> nextnode = GetNextNode();
            foreach(Node n in nextnode)
            {
                print("NEXT NODE IS "+n.nodeType);
            }
        }
    }
}
