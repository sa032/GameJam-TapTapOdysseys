using System.Collections.Generic;
using System.Diagnostics;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Map
{
    public class MapNodeSelectUI : MonoBehaviour
    {
        public MapManager mapManager;
        public MapViewUI mapView;
        public GameObject Card1,Card2,Card3;
        
        List<Node> GetNextNode()
        {
            List<Node> a = new List<Node>();
            
            if(mapManager.CurrentMap.path.Count == 0)
            {
                a.Add(mapManager.CurrentMap.GetNode(new Vector2Int(0,0)));
                return a;
            }else{
                Vector2Int currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);
                foreach(Vector2Int n in currentNode.outgoing)
                {
                    a.Add(mapManager.CurrentMap.GetNode(n));
                }
                return a;
            }
        }
        
        public void GetNextNodeUI()
        {
            List<Node> nextnode = GetNextNode();
            
            CardUI(nextnode[0],Card1);
            if(nextnode.Count > 1)CardUI(nextnode[1],Card2);
        }
        void CardUI(Node nextnode , GameObject Card)
        {
            TextMeshProUGUI TextTitle = Card.transform.Find("Title").Find("Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI TextDescription = Card.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            Image image = Card.transform.Find("Image").GetComponent<Image>();
            
            TextTitle.text = nextnode.nodeType.ToString();
            image.sprite = GetBlueprint(nextnode.blueprintName).sprite;
            Card.GetComponent<CardContainData>().NodeData = FindMapNode(nextnode.point);
        }
        protected NodeBlueprint GetBlueprint(string blueprintName)
        {
            MapConfig config = GetConfig(mapManager.CurrentMap.configName);
            return config.nodeBlueprints.FirstOrDefault(n => n.name == blueprintName);
        }
        protected MapConfig GetConfig(string configName)
        {
            return mapView.allMapConfigs.FirstOrDefault(c => c.name == configName);
        }

        MapNode FindMapNode(Vector2Int pos)
        {
            GameObject[] NodeUI = GameObject.FindGameObjectsWithTag("NodeUI");
            MapNode Result = null;
            foreach(GameObject g in NodeUI)
            {
                MapNode MapN = g.GetComponent<MapNode>();
                if(MapN.Pos == pos){Result = MapN;break;}
            }
            return Result;
        }

        public void SendPlayerNextNode(MapNode mapNode)
        {
            MapPlayerTracker.Instance.SendPlayerToNode(mapNode);
        }

        public void ButtonTest(GameObject Button)
        {
            MapNode mp = Button.transform.parent.GetComponent<CardContainData>().NodeData;
            SendPlayerNextNode(mp);
        }
    }
}
