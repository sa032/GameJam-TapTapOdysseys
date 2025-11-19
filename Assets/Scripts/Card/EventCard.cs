using System.Collections;
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
        
    }
    public void EnterNodeEvent(Node node)
    {
        switch (node.nodeType)
        {
            case NodeType.Enemy :
                print("ENEMY APPEAR >:)");
                break;
            case NodeType.Treasure :
                print("TREASURE HELL YEAH!!!");
                CardAmount = Random.Range(1,4);
                if(ItemManager.instance.items.Count < CardAmount) CardAmount = ItemManager.instance.items.Count;

                for(int i = 0;i<CardAmount;i++){
                    GameObject ChestClone = Instantiate(ChestPrefab,ObjectContainer.transform);
                    ChestClone.transform.localPosition = PositionSet.transform.Find("ChestPos").localPosition + 
                    new Vector3(i*3f,0,0);

                }
                TreasureEvent(timer+0.1f);
                break;
        }
    }
    public void TreasureEvent(float delayTime)
    {
        
        StartCoroutine(TreasureAnimation(delayTime));
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
                    TreasureEvent(timer+0.1f);
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
