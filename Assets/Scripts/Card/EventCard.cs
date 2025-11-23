using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Map;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventCard : MonoBehaviour
{
    public static EventCard Instance;
    public int CardAmount;
    GameObject MapObj;
    GameObject ObjectContainer;
    GameObject PositionSet;
    [Header("Prefab")]
    public GameObject VFXItemCollect;
    public GameObject ChestPrefab;
    public GameObject TreasureUI;
    public GameObject NPCEncounterUI;
    [Header("Value")]
    public bool isEnterEnemyNode;
    float timer = 0.5f;
    void Start()
    {
        MapObj = GameObject.FindGameObjectWithTag("Map");
        ObjectContainer = MapObj.transform.Find("ObjectContainer").gameObject;
        PositionSet = MapObj.transform.Find("PositionSet").gameObject;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnterEnemyNode != true) return;
        if(EnemyManager.instance.enemies.Count == 0)
        {
            StartCoroutine(EnemyReward());
            isEnterEnemyNode = false;
        }
    }
    public NodeType EnemyNodeType;
    public void EnterNodeEvent(Node node)
    {
        switch (node.nodeType)
        {
            case NodeType.Enemy :
                EnemyNodeType = NodeType.Enemy;
                print("ENEMY APPEAR >:)");
                StartCoroutine(SpawnMinorEnemy());
                break;
            case NodeType.EliteEnemy :
                EnemyNodeType = NodeType.EliteEnemy;
                StartCoroutine(SpawnEliteEnemy());
                break;
            case NodeType.Treasure :
                print("TREASURE HELL YEAH!!!");
                
                TreasureEvent(timer+0.1f,3);
                break;
            case NodeType.Encounter :
                EncounterNPC();
                break;
            case NodeType.Rest :
                StartCoroutine(RestAnimation());
                break;
            case NodeType.Boss :
            EnemyNodeType = NodeType.Boss;
                StartCoroutine(SpawnBossEnemy());
                break;
        }
    }
    public ParticleSystem Healing;
    IEnumerator RestAnimation()
    {
        SoundManager.instance.PlaySoundSFX("Healing");
        Healing.Play();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Health>().health = Player.GetComponent<Health>().maxHealth; 
        yield return new WaitForSeconds(1f);
        MapNodeSelectUI.instance.GetNextNodeUI();
    }
    IEnumerator EnemyReward()
    {
        yield return new WaitForSeconds(0.75f);
        if (EnemyNodeType == NodeType.Enemy)
        {
            
            int RandomTresure = Random.Range(0,10);

            if (RandomTresure == 5){
                TreasureUI.SetActive(true);
                SoundManager.instance.PlaySoundSFX("TreasureDrop");
                yield return new WaitForSeconds(0.75f);
                TreasureUI.SetActive(false);
                TreasureEvent(timer+0.1f,1);
            }
            else MapNodeSelectUI.instance.GetNextNodeUI();

        }else if (EnemyNodeType == NodeType.EliteEnemy)
        {
            int RandomTresure = Random.Range(0,2);
            
            if (RandomTresure == 1){
                TreasureUI.SetActive(true);
                SoundManager.instance.PlaySoundSFX("TreasureDrop");
                yield return new WaitForSeconds(0.75f);
                TreasureUI.SetActive(false);
                TreasureEvent(timer+0.1f,1);
            }
            else MapNodeSelectUI.instance.GetNextNodeUI();
        }else if (EnemyNodeType == NodeType.Boss)
        {
            TreasureUI.SetActive(true);
            SoundManager.instance.PlaySoundSFX("TreasureDrop");
            yield return new WaitForSeconds(0.75f);
            TreasureUI.SetActive(false);
            TreasureEvent(timer+0.1f,3);
        }
        
    }
    public void TreasureEvent(float delayTime,int MaxRandomChestAmount)
    {
        CardAmount = Random.Range(1,MaxRandomChestAmount+1);
        if(ItemManager.instance.items.Count < CardAmount) CardAmount = ItemManager.instance.items.Count;

        for(int i = 0;i<CardAmount;i++){
            GameObject ChestClone = Instantiate(ChestPrefab,ObjectContainer.transform);
            ChestClone.transform.localPosition = PositionSet.transform.Find("ChestPos").localPosition + 
            new Vector3(i*1.5f,0,0);

        }
        StartCoroutine(TreasureAnimation(delayTime));
    }
    public GameObject BossBar;
    public IEnumerator SpawnBossEnemy()
    {
        yield return new WaitForSeconds(0.25f);
        BossBar.SetActive(true);
        isEnterEnemyNode = true;
        int randomEnemyAmount = Random.Range(1,5);
        MapManager map = MapManager.Instance;
        List<GameObject> EnemyToClone = new List<GameObject>();
        
        List<GameObject> Enemy = map.FloorDataConfig.FloorData[map.CurrentFloor].BossPrefab;
        EnemyToClone.Add(Enemy[0]);
        
        EnemyManager.instance.SpawnEnemies(EnemyToClone);
        BossBar.GetComponent<HealthBarPlayer>().playerHP = EnemyManager.instance.enemies[0].GetComponent<Health>();
    }
    public IEnumerator SpawnMinorEnemy()
    {
        yield return new WaitForSeconds(0.25f);
        isEnterEnemyNode = true;
        int randomEnemyAmount = Random.Range(1,5);
        MapManager map = MapManager.Instance;
        List<GameObject> EnemyToClone = new List<GameObject>();
        
        for (int i =0;i<randomEnemyAmount;i++)
        {
            List<GameObject> Enemy = map.FloorDataConfig.FloorData[map.CurrentFloor].EnemyPrefab;
            int randomEnemy = Random.Range(0,Enemy.Count);
            EnemyToClone.Add(Enemy[randomEnemy]);
            
        }
        EnemyManager.instance.SpawnEnemies(EnemyToClone);
    }
    public IEnumerator SpawnEliteEnemy()
    {
        yield return new WaitForSeconds(0.25f);
        isEnterEnemyNode = true;
        int randomEnemyAmount = Random.Range(1,5);
        MapManager map = MapManager.Instance;
        List<GameObject> EnemyToClone = new List<GameObject>();
        
        for (int i =0;i<randomEnemyAmount;i++)
        {
            List<GameObject> EliteEnemy = map.FloorDataConfig.FloorData[map.CurrentFloor].EliteEnemyPrefab;
            int randomEnemy = Random.Range(0,EliteEnemy.Count);
            EnemyToClone.Add(EliteEnemy[randomEnemy]);
            
        }
        EnemyManager.instance.SpawnEnemies(EnemyToClone);
    }
    public GameObject BorderUI2;
    void EncounterNPC()
    {
        
        MapManager map = MapManager.Instance;
        FloorDataPool data = map.FloorDataConfig.FloorData[map.CurrentFloor];
        int randomEncounter = Random.Range(0,data.encounters.Count);
        Encounter encounter = data.encounters[randomEncounter];
        NPCEncounterUI.SetActive(true);
        NPCEncounterUI.GetComponent<Image>().sprite = encounter.imageNPC;
        BorderUI2.SetActive(true);
        TextMeshProUGUI text = NPCEncounterUI.transform.Find("ChatBubble").Find("Text").GetComponent<TextMeshProUGUI>();
        TimeBarDatasets.TimedEventDataset events = TimeBarManager.instance.currentDataset.datasets[2];
        text.text = encounter.dialouge;
        events.elements[1].minTime = 0;
        events.elements[1].maxTime = 50;

        events.elements[2].minTime = 50;
        events.elements[2].maxTime = 100;

        events.elements[0].minTime = 0;
        events.elements[0].maxTime = 0;
        TimeBarManager.instance.SwitchDataset(2);
        
        MapNodeSelectUI.instance.Card1.GetComponent<CardContainData>().state = CardState.EncounterChoice;
        MapNodeSelectUI.instance.Card1.GetComponent<CardContainData>().encounterType = EncounterType.None;
        
        MapNodeSelectUI.instance.Card2.GetComponent<CardContainData>().state = CardState.EncounterChoice;
        MapNodeSelectUI.instance.Card2.GetComponent<CardContainData>().encounterType = EncounterType.Exp;
        StartCoroutine(showcard());
        SoundManager.instance.PlaySoundSFX("NPCEnter");
        

    }
    IEnumerator showcard()
    {
        yield return new WaitForSeconds(1f);
        MapNodeSelectUI.instance.Card1.SetActive(true);
        MapNodeSelectUI.instance.Card2.SetActive(true);
    }
    public void ExitEncounter()
    {
        BorderUI2.SetActive(false);
        NPCEncounterUI.SetActive(false);
        StartCoroutine(GonextNode());
    }
    IEnumerator GonextNode()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        foreach(GameObject card in cards) card.SetActive(false);

        MapNodeSelectUI.instance.GetNextNodeUI();
    }
    IEnumerator TreasureAnimation(float delayTime)
    {
        yield return null;
        GameObject[] ChestClone = GameObject.FindGameObjectsWithTag("Chest");
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0;i<ItemManager.instance.GetRandomItems().Count;i++)
        {
            GameObject VFXItemCollect_Clone = Instantiate(VFXItemCollect);
            VFXItemCollect_Clone.transform.position = ChestClone[CardAmount-1].transform.position;
            float y = MapNodeSelectUI.instance.Card1.transform.position.y;
            if(i == 0){
                Vector3 Cardpos = MapNodeSelectUI.instance.Card1.transform.position;
                Vector3 pos = new Vector3(Cardpos.x,y,Cardpos.z);
                LeanTween.moveLocal(VFXItemCollect_Clone,pos,timer);
            }
            if(i == 1){
                Vector3 Cardpos = MapNodeSelectUI.instance.Card2.transform.position;
                Vector3 pos = new Vector3(Cardpos.x,y,Cardpos.z);
                LeanTween.moveLocal(VFXItemCollect_Clone,pos,timer);
            }
            if(i == 2){
                Vector3 Cardpos = MapNodeSelectUI.instance.Card3.transform.position;
                Vector3 pos = new Vector3(Cardpos.x,y,Cardpos.z);
                LeanTween.moveLocal(VFXItemCollect_Clone,pos,timer);
            }
            
            Destroy(VFXItemCollect_Clone,timer+0.1f);
        }
        if(ChestClone.Length > 0) ChestClone[CardAmount-1].GetComponent<Animator>().SetBool("Open",true);
        SoundManager.instance.PlaySoundSFX("ChestOpen2");
        yield return new WaitForSeconds(delayTime);
        SoundManager.instance.PlaySoundSFX("ChestOpen");
        MapNodeSelectUI.instance.TreasureUI();
        
    }
    public void CardCollected(CardState cardState)
    {
        ResetCard();
        if(CardAmount > 1){
            CardAmount-=1;
            switch(cardState){
                case CardState.Item:
                    StartCoroutine(TreasureAnimation(0.5f));
                    break;
            }
        }
        else
        {
            MapNodeSelectUI.instance.GetNextNodeUI();
        }

    }
    void ResetCard()
    {
        GameObject[] Cards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject c in Cards)
        {
            c.SetActive(false);
        }
    }
    public void ClearObject()
    {
        for (int i = 0;i < ObjectContainer.transform.childCount;i++)
        {
            Destroy(ObjectContainer.transform.GetChild(i).gameObject);    
        }
    }
}
