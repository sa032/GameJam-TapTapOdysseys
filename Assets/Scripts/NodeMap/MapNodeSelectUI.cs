using System.Collections.Generic;
using System.Diagnostics;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;

namespace Map
{
    public class MapNodeSelectUI : MonoBehaviour
    {
        public MapManager mapManager;
        public static MapNodeSelectUI instance;
        public MapViewUI mapView;
        public GameObject Card1,Card2,Card3,Title;
        void Start()
        {
            PreviousLevel = LevelManager.instance.currentLevel;
            instance = this;
        }
        List<Node> GetNextNode()
        {
            List<Node> a = new List<Node>();
            
            if(mapManager.CurrentMap.path.Count == 0)
            {
                if(mapManager.CurrentMap.GetNode(new Vector2Int(0,0)) != null)
                a.Add(mapManager.CurrentMap.GetNode(new Vector2Int(0,0)));
                else a.Add(mapManager.CurrentMap.GetNode(new Vector2Int(1,0)));
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
        public GameObject NextUI;
        public int PreviousLevel = 1;
        public void GetDifLevel()
        {
            int newLevel = LevelManager.instance.predictedLevel;
            int Dif_level = newLevel - PreviousLevel;
            LevelManager.instance.Dif_level = Dif_level;
            
        }
        public void GetNextNodeUI()
        {
            MapConfig config = mapManager.config;
            PreviousLevel = LevelManager.instance.predictedLevel;
            MagicCardManager.instance.BorderDB();
            if(LevelManager.instance.Dif_level == 0){
                if(mapManager.CurrentMap.path.Count < config.FloorLayers[mapManager.CurrentFloor].layers.Count){
                    List<Node> nextnode = GetNextNode();
                    //CardUI(nextnode[0],Card1);
                    StartCoroutine(NextUIOutTransition());
                    //if(nextnode.Count > 1)CardUI(nextnode[1],Card2);
                    if(nextnode.Count > 1){
                        Node NodeUp = null;
                        Node NodeDown = null;
                        
                        foreach(Node node in nextnode)
                        {
                            if(node.point.x == 1){NodeUp = node;}
                            else NodeDown = node;
                        }

                        if(NodeUp != null)CardUI(NodeUp,Card2);
                        if(NodeDown != null)CardUI(NodeDown,Card1);;
                    }
                    else
                    {
                        CardUI(nextnode[0],Card1);
                    }
                    

                    ShowSelectPathUI(nextnode);
                    
                }
                else
                {
                    if (mapManager.CurrentFloor >= mapManager.config.FloorLayers.Count-1)
                    {
                        Victory.SetActive(true);
                    }else{
                        TimeBarDatasets.TimedEventDataset events = TimeBarManager.instance.currentDataset.datasets[2];
                        events.elements[1].minTime = 0;
                        events.elements[1].maxTime = 0;

                        events.elements[2].minTime = 0;
                        events.elements[2].maxTime = 100;

                        events.elements[0].minTime = 0;
                        events.elements[0].maxTime = 0;
                        TimeBarManager.instance.SwitchDataset(2);
                        Card1.GetComponent<CardContainData>().state = CardState.GoNextFloor;
                        Card1.SetActive(true);
                    }
                }
            }
            else
            {
                
                MagicCardManager.instance.BorderShow(new Color(1.000f, 0.898f, 0.588f, 1.000f));
                LevelUPSelectCard();
            }
        }
        public GameObject Victory; 
        void CardUI(Node nextnode , GameObject Card)
        {
            Card.GetComponent<CardContainData>().state = CardState.SelectPath;
            Card.GetComponent<CardContainData>().NodeData = FindMapNode(nextnode.point);
        }
        IEnumerator NextUIOutTransition()
        {

            NextUI.SetActive(false);
            NextUI.SetActive(true);
            yield return new WaitForSeconds(1f);
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
        //TODO: --------------------------------------------------------------EVENT-----------------
        List<GameObject> items;
        List<MagicBase> magics;
        public void TreasureUI()
        {
            items = ItemManager.instance.GetRandomItems();
            int Amount = items.Count;
            ShowCardUI(Amount,NodeType.Treasure);
        }
        public void LevelUPSelectCard()
        {
            magics = MagicCardManager.instance.GetRandomMagic();
            int Amount = magics.Count;
            ShowCardUI(Amount,NodeType.None);
        }
        public void ShowCardUI(int CardAmount , NodeType nodeStates)
        {
            List<GameObject> Cards = new List<GameObject>();
            if(CardAmount > 0) Cards.Add(Card1);
            if(CardAmount >= 2) Cards.Add(Card2);
            if(CardAmount >= 3) Cards.Add(Card3);
            
            int i = 0;
            foreach(GameObject card in Cards)
            {
                if(nodeStates == NodeType.Treasure)
                {
                    Title.SetActive(false);
                    CardContainData cardContainData = card.GetComponent<CardContainData>();
                    cardContainData.PrefabItem = items[i];
                    cardContainData.state = CardState.Item;
                    card.SetActive(true);
                }
                if(nodeStates == NodeType.None)
                {
                    Title.SetActive(true);
                    Title.GetComponent<TextMeshProUGUI>().text = "LEVEL UP CHOOSE SKILL ("+LevelManager.instance.Dif_level+")";
                    CardContainData cardContainData = card.GetComponent<CardContainData>();
                    cardContainData.MagicData = magics[i];
                    cardContainData.state = CardState.Magic;
                    card.SetActive(true);
                }
                i++;
            }
            TimeDatasetSwitch(CardAmount);
            if(CardAmount == 0)
            {
                GetNextNodeUI();
            }
        }
        public void TimeDatasetSwitch(int CardAmount)
        {
            ParryMode.instance.exitParryMode();
            
            TimeBarDatasets.TimedEventDataset events = TimeBarManager.instance.currentDataset.datasets[2];
            if (CardAmount == 3)
            {
                events.elements[0].minTime = 0;
                events.elements[0].maxTime = 34;

                events.elements[1].minTime = 34;
                events.elements[1].maxTime = 67;

                events.elements[2].minTime = 67;
                events.elements[2].maxTime = 100;
            }else if (CardAmount == 2)
            {
                events.elements[1].minTime = 0;
                events.elements[1].maxTime = 50;

                events.elements[2].minTime = 50;
                events.elements[2].maxTime = 100;

                events.elements[0].minTime = 0;
                events.elements[0].maxTime = 0;

            }else if (CardAmount == 1)
            {
                events.elements[2].minTime = 0;
                events.elements[2].maxTime = 100;

                events.elements[1].minTime = 0;
                events.elements[1].maxTime = 0;

                events.elements[0].minTime = 0;
                events.elements[0].maxTime = 0;
            }
            TimeBarManager.instance.SwitchDataset(2);
        }
        //TODO: --------------------------------------------------------------EVENT-----------------
        public void ShowSelectPathUI(List<Node> nextnode)
        {
            DisibleCard();
            if(nextnode != null){
                TimeDatasetSwitch(nextnode.Count);
                if(nextnode.Count > 1)
                {
                    Card1.SetActive(true);
                    Card2.SetActive(true);
                }
                else
                {
                    Card1.SetActive(true);
                }
            }
        }

        void DisibleCard()
        {
            Card1.SetActive(false);
            Card2.SetActive(false);
            Card3.SetActive(false);
        }
    }
}
