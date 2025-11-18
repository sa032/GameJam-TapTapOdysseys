using Map;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;

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
    public GameObject PrefabItem;
    public CardState state;
    public GameObject VFXItemCollect;
    public GameObject InventoryItemUI;
    GameObject[] AllCards;
    GameObject NextUI;

    public void Execute()
    {
        if(NodeData != null)nodeSave = NodeData.Node;
        switch (state)
        {
            case CardState.SelectPath:
                SendPlayerNextNode(NodeData);   
                Reset();        
                EnterNodeTransition();
                break; 
            case CardState.GoNextFloor:
                print("GO TO NEXT FLOOR");
                break;
            case CardState.Item:
                StartCoroutine(GetItem());
                break;
        }
        
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
        CardAnimationTransition();
        CardUI();
    }
    void CardUI()
    {
        TextMeshProUGUI TextTitle = transform.Find("CardContainer").Find("Title").Find("Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI TextDescription = transform.Find("CardContainer").Find("Description").GetComponent<TextMeshProUGUI>();
        Image image = transform.Find("CardContainer").Find("Image").GetComponent<Image>();
        if(state == CardState.SelectPath){
            NodeBlueprint blueprint = GetBlueprint(NodeData.Node.blueprintName);
            TextDescription.text = blueprint.description.ToString();
            TextTitle.text = blueprint.nodeType.ToString();
            image.sprite = blueprint.sprite;
        }else if (state == CardState.Item)
        {   
            ItemDataContain itemDataContain = PrefabItem.GetComponent<ItemDataContain>();
            TextDescription.text = itemDataContain.description.ToString();
            TextTitle.text = itemDataContain.Name.ToString();
            image.sprite = itemDataContain.image;
        }
    }
    IEnumerator GetItem()
    {
        
        ItemManager.instance.AddItemToInventory(PrefabItem);
        Reset();
        GameObject VFXItemCollect_Clone = Instantiate(VFXItemCollect);
        VFXItemCollect_Clone.transform.position = this.transform.position;
        yield return new WaitForSeconds(0.15f);
        LeanTween.moveLocal(VFXItemCollect_Clone,InventoryItemUI.transform.position,0.25f);
        yield return new WaitForSeconds(0.2f);
        //VFXItemCollect_Clone.transform.GetChild(0).gameObject.SetActive(false);
        VFXItemCollect_Clone.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.5f);
        VFXItemCollect_Clone.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Destroy(VFXItemCollect_Clone);
        EventCard.Instance.CardCollected(CardState.Item);
        //!MapNodeSelectUI.instance.GetNextNodeUI();
    }
    void CardAnimationTransition()
    {
        StartCoroutine(CardTransition());
    }
    IEnumerator CardTransition()
    {
        float delaytime = this.GetComponent<DoAnimationEnable>().DelayTime;
        Image[] images = GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            //if(image != this.GetComponent<Image>()){
                Color temp = image.color;
                image.color = new Color(temp.r,temp.g,temp.b,(float)0f);
            //}
        }
        TextMeshProUGUI[] Text_tmp = GetComponentsInChildren<TextMeshProUGUI>();
        foreach(TextMeshProUGUI text in Text_tmp)
        {
            Color temp = text.color;
            text.color = new Color(temp.r,temp.g,temp.b,(float)0f);
        }
        yield return new WaitForSeconds(delaytime);
        for (int i = 0;i<25;i++)
        {
            foreach(Image image in images)
            {
                //if(image != this.GetComponent<Image>()){
                    Color temp = image.color;
                    image.color = new Color(temp.r,temp.g,temp.b,(float)i/25f);
                //}
            }
            foreach(TextMeshProUGUI text in Text_tmp)
            {
                Color temp = text.color;
                text.color = new Color(temp.r,temp.g,temp.b,(float)i/25f);
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
    void Reset()
    {
        AllCards = GameObject.FindGameObjectsWithTag("Card");
        foreach(GameObject card in AllCards)
        {
            if(card != this.gameObject){
                card.GetComponent<CardContainData>().state = CardState.None;
                card.GetComponent<CardContainData>().NodeData = null;
                //card.SetActive(false);
                card.GetComponent<CardContainData>().CardUI_TransitionOut(false);
            }
        }
        state = CardState.None;
        NodeData = null;

        NextUI = GameObject.FindGameObjectWithTag("NextUI");
        //this.gameObject.SetActive(false);
        CardUI_TransitionOut(true);
        
    }
    void EnterNodeTransition()
    {
        StartCoroutine(NextUIOutTransition_EnterNode());
    }
    Node nodeSave;
    IEnumerator NextUIOutTransition_EnterNode()
    {
        GameObject BlackScreen = GameObject.FindGameObjectsWithTag("BlackScene")[0].transform.GetChild(0).gameObject;
        Animator anim = NextUI.GetComponent<Animator>();
        anim.Play("NextUI_Out");
        BlackScreen.SetActive(false);
        yield return null;
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f
        );   
        
        BlackScreen.SetActive(true);
        yield return new WaitUntil(() =>
            BlackScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f
        );
        EventCard.Instance.ClearObject();
        EventCard.Instance.EnterNodeEvent(nodeSave);
        yield return new WaitUntil(() =>
            BlackScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        );
        NextUI.SetActive(false);
        BlackScreen.SetActive(false);
        this.gameObject.SetActive(false);   
    }
    
    public void CardUI_TransitionOut(bool db)
    {
        if(db == true) this.GetComponent<Animator>().Play("CardAnim2_SelectPath");
        StartCoroutine(CardTransitionOut(db));
    }
    IEnumerator CardTransitionOut(bool main)
    {
        float delaytime = this.GetComponent<DoAnimationEnable>().DelayTime;
        Image[] images = GetComponentsInChildren<Image>();
        TextMeshProUGUI[] Text_tmp = GetComponentsInChildren<TextMeshProUGUI>();
        yield return new WaitForSeconds(0.035f);
        for (int i = 0;i<=25;i++)
        {
            foreach(Image image in images)
            {
                //if(image != this.GetComponent<Image>()){
                    Color temp = image.color;
                    image.color = new Color(temp.r,temp.g,temp.b,1f-(float)i/25f);
                //}
            }
            foreach(TextMeshProUGUI text in Text_tmp)
            {
                Color temp = text.color;
                text.color = new Color(temp.r,temp.g,temp.b,1f-(float)i/25f);
            }
            yield return new WaitForSeconds(0.005f);
        }
        if(main == false) this.gameObject.SetActive(false);
        
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
