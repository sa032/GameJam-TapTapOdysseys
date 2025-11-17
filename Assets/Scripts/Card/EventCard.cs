using System.Collections;
using Map;
using UnityEngine;

public class EventCard : MonoBehaviour
{
    public static EventCard Instance;
    void Start()
    {
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
                TreasureEvent();
                break;
        }
    }
    public void TreasureEvent()
    {
        StartCoroutine(TreasureAnimation());
    }
    IEnumerator TreasureAnimation()
    {
        yield return new WaitForSeconds(2f);
        MapNodeSelectUI.instance.TreasureUI();
    }
}
