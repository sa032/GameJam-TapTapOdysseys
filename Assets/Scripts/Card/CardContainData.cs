using Map;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public enum CardState
{
    None,
    SelectPath,
    GoNextFloor,
    Item,
    ShopItem,
    EncounterChoice,
}
public class CardContainData : MonoBehaviour
{
    public MapNode NodeData;
    public CardState state;
    GameObject[] AllCards;

    public void Execute()
    {
        switch (state)
        {
            case CardState.SelectPath:
                SendPlayerNextNode(NodeData);           
                break; 
            case CardState.GoNextFloor:
                print("GO TO NEXT FLOOR");
                break;
        }
        Reset();
    }
    void Awake()
    {
        AllCards = GameObject.FindGameObjectsWithTag("Card");
    }
    public void SendPlayerNextNode(MapNode mapNode)
    {
        MapPlayerTracker.Instance.SendPlayerToNode(mapNode);
    }
    void OnEnable()
    {
        CardUI();
    }
    void CardUI()
    {
        TextMeshProUGUI TextTitle = transform.Find("Title").Find("Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI TextDescription = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        Image image = transform.Find("Image").GetComponent<Image>();
        
        TextTitle.text = GetBlueprint(NodeData.Node.blueprintName).nodeType.ToString();
        image.sprite = GetBlueprint(NodeData.Node.blueprintName).sprite;
    }
    void Reset()
    {
        AllCards = GameObject.FindGameObjectsWithTag("Card");
        foreach(GameObject card in AllCards)
        {
            if(card != this.gameObject){
                card.GetComponent<CardContainData>().state = CardState.None;
                card.GetComponent<CardContainData>().NodeData = null;
                card.SetActive(false);
            }
        }
        state = CardState.None;
        NodeData = null;
        this.gameObject.SetActive(false);
    }

    protected NodeBlueprint GetBlueprint(string blueprintName)
    {
        MapConfig config = GetConfig(MapManager.Instance.CurrentMap.configName);
        return config.nodeBlueprints.FirstOrDefault(n => n.name == blueprintName);
    }
    protected MapConfig GetConfig(string configName)
    {
        return MapView.Instance.allMapConfigs.FirstOrDefault(c => c.name == configName);
    }
    
}
