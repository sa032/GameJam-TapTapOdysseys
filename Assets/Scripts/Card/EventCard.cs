using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Map;
using Unity.VisualScripting;
using UnityEngine;

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
        }
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
        yield return new WaitForSeconds(delayTime);
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
