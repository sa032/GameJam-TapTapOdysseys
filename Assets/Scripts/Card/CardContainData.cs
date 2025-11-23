using Map;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;
using Unity.Mathematics;

public enum CardState
{
    None,
    SelectPath,
    GoNextFloor,
    Item,
    Magic,
    ShopItem,
    EncounterChoice,
    MagicSwitch,
}
public class CardContainData : MonoBehaviour
{
    public MapNode NodeData;
    public GameObject PrefabItem;
    public MagicBase MagicData;
    public CardState state;
    public GameObject VFXItemCollect;
    public EncounterType encounterType;
    public GameObject InventoryItemUI;
    public static CardContainData instance;
    GameObject[] AllCards;
    GameObject NextUI;
    public System.Action CardEvent;
    Color[] ColorRarity =
    {
        new Color(0.706f, 0.706f, 0.706f, 1.000f),
        new Color(0.647f, 0.824f, 1.000f, 1.000f),
        new Color(0.914f, 0.757f, 1.000f, 1.000f),
        new Color(1.000f, 0.894f, 0.580f, 0.989f)
    };

    public void Execute()
    {
        if(NodeData != null)nodeSave = NodeData.Node;
        TimeBarManager.instance.SwitchDataset(0);
        switch (state)
        {
            case CardState.SelectPath:
                SendPlayerNextNode(NodeData);   
                Reset();        
                SoundManager.instance.PlaySoundSFX("NextStage");
                EnterNodeTransition();
                break; 
            case CardState.GoNextFloor:
                print("GO TO NEXT FLOOR");
                StartCoroutine(NextFloorEnter());
                Reset();
                break;
            case CardState.Item:
                StartCoroutine(GetItem());
                break;
            case CardState.Magic :
                MagicCardManager.instance.AddMagicToInventory(MagicData);
                MagicCardManager.instance.CardPreviewFunc(MagicData,this.transform.position);
                
                Reset();
                //StartCoroutine(DisbleSelf(0.5f));
                break;
            case CardState.MagicSwitch:
                int index = 0;
                if(this.gameObject.name == "Card1") index = 2;
                if(this.gameObject.name == "Card2") index = 1;
                if(this.gameObject.name == "Card3") index = 0;
                
                MagicCardManager.instance.SwicthMagic(index);
                
                Reset();
                break;
            case CardState.EncounterChoice:
                if (encounterType == EncounterType.None)
                {
                    EventCard.Instance.ExitEncounter();
                }
                else if (encounterType == EncounterType.Exp)
                {
                    LevelManager.instance.AddExp(LevelManager.instance.expToNextLevel+1);
                    EventCard.Instance.ExitEncounter();
                }
                
                Reset();
                break;
            
        }
        
    }
    
    void Awake()
    {
        instance = this;
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
        Transform CardContainer = transform.Find("CardContainer");
        Transform CardSkillContainer = transform.Find("CardSkillContainer");

        TextMeshProUGUI TextTitle = CardContainer.Find("Title").Find("Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI TextDescription = CardContainer.Find("Description").GetComponent<TextMeshProUGUI>();
        Image image = CardContainer.Find("Image").GetComponent<Image>();

        TextMeshProUGUI TextTitleMagic = CardSkillContainer.Find("Title").Find("Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI TextDescriptionMagic = CardSkillContainer.Find("Description").GetComponent<TextMeshProUGUI>();
        Image imageMagic = CardSkillContainer.Find("Image").GetComponent<Image>();
        Image TypeOfMagicUI = CardSkillContainer.Find("TypeOfMagicUI").GetComponent<Image>();

        Outline outline = this.GetComponent<Outline>();
        outline.enabled = false;
        CardContainer.gameObject.SetActive(false);
        CardSkillContainer.gameObject.SetActive(false);
        this.GetComponent<Image>().color = new Color(1,1,1,1);
        if(state == CardState.SelectPath){
            CardContainer.gameObject.SetActive(true);
            NodeBlueprint blueprint = GetBlueprint(NodeData.Node.blueprintName);
            TextDescription.text = blueprint.description.ToString();
            TextTitle.text = blueprint.nodeType.ToString();
            image.sprite = blueprint.sprite;
        }else if (state == CardState.Item)
        {   
            CardContainer.gameObject.SetActive(true);
            ItemDataContain itemDataContain = PrefabItem.GetComponent<ItemDataContain>();
            TextDescription.text = itemDataContain.description.ToString();
            TextTitle.text = itemDataContain.Name.ToString();
            image.sprite = itemDataContain.image;

            outline.enabled = true;
            outline.effectColor = ColorRarity[(int)itemDataContain.Rarity];
        }else if (state == CardState.Magic || state == CardState.MagicSwitch)
        {
            this.GetComponent<Image>().color = new Color(0.239f, 0.239f, 0.239f, 1.000f);
            CardSkillContainer.gameObject.SetActive(true);
            MagicBase itemDataContain = MagicData;
            TextDescriptionMagic.text = MagicData.Description.ToString();
            TextTitleMagic.text = "["+MagicData.magicType.ToString()+"] "+MagicData.Name.ToString();
            imageMagic.sprite = MagicData.Image;

            outline.enabled = true;
            outline.effectColor = ColorRarity[(int)itemDataContain.Rarity];

            if (MagicData.magicType == MagicType.Passive)
            {
                TypeOfMagicUI.color = new Color(0.514f, 0.545f, 1.000f, 1.000f);
            }
            else
            {
                TypeOfMagicUI.color = new Color(1.000f, 0.098f, 0.310f, 1.000f);
            }
        }else if (state == CardState.EncounterChoice)
        {
            CardContainer.gameObject.SetActive(true);
            TextDescription.text = "";
            if (encounterType == EncounterType.None)
            {
                TextTitle.text = "No";
                image.sprite = No;
            }else
            {
                TextTitle.text = "Yes";
                image.sprite = Yes;
            }
            
        }else if (state == CardState.GoNextFloor)
        {
            CardContainer.gameObject.SetActive(true);
            TextTitle.text = "Next Floor";
            TextDescription.text = "Go to the next dimension";
            image.sprite = NextFloor;
        }
    }
    public Sprite Yes,No,NextFloor;
    IEnumerator GetItem()
    {
        
        ItemManager.instance.AddItemToInventory(PrefabItem);
        Reset();
        GameObject VFXItemCollect_Clone = Instantiate(VFXItemCollect);
        VFXItemCollect_Clone.transform.position = this.transform.position;
        yield return new WaitForSeconds(0.15f);
        SoundManager.instance.PlaySoundSFX("ItemGet1");
        LeanTween.moveLocal(VFXItemCollect_Clone,InventoryItemUI.transform.position,0.25f);
        yield return new WaitForSeconds(0.2f);
        //VFXItemCollect_Clone.transform.GetChild(0).gameObject.SetActive(false);
        SoundManager.instance.PlaySoundSFX("ItemGet2");
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
    public ParticleSystem DimensionVFX;
    public IEnumerator DisbleSelf(float db)
    {
        yield return new WaitForSeconds(db);
        this.gameObject.SetActive(false);
    }
    public IEnumerator NextFloorEnter()
    {
        GameObject BlackScreen = GameObject.FindGameObjectsWithTag("BlackScene")[0].transform.GetChild(0).gameObject;
        //Animator anim = NextUI.GetComponent<Animator>();
        //anim.Play("NextUI_Out");
        BlackScreen.SetActive(false);
        yield return null;
        //yield return new WaitUntil(() =>
            //anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f
        //);   
        
        BlackScreen.SetActive(true);
        yield return new WaitUntil(() =>
            BlackScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f
        );
        EventCard.Instance.ClearObject();
        //TODO >>>>>>
        if (MapManager.Instance.CurrentFloor+1 < MapManager.Instance.config.FloorLayers.Count)
        {
            DimensionVFX.Play();
            SoundManager.instance.PlaySoundSFX("DimensionVFX");
            //print("LET TO THE NEXT FLOOR");
            MapManager.Instance.CurrentFloor += 1;
            MapManager.Instance.GenerateNewMap();
        }
        
        yield return new WaitUntil(() =>
            BlackScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        );
        MapNodeSelectUI.instance.Card1.SetActive(false);
        MapNodeSelectUI.instance.Card2.SetActive(false);
        MapNodeSelectUI.instance.Card3.SetActive(false);
        //NextUI.SetActive(false);
        BlackScreen.SetActive(false);
        this.gameObject.SetActive(false);   
        MapNodeSelectUI.instance.GetNextNodeUI();
    }
    void EnterNodeTransition()
    {
        StartCoroutine(NextUIOutTransition_EnterNode());
    }
    Node nodeSave;
    public IEnumerator NextUIOutTransition_EnterNode()
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
        if(db == true && this.gameObject.activeSelf)this.GetComponent<Animator>().Play("CardAnim2_SelectPath");
        if (gameObject.activeInHierarchy) StartCoroutine(CardTransitionOut(db));
    }
    public IEnumerator CardTransitionOut(bool main)
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
