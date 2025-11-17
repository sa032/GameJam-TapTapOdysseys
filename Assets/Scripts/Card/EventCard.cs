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
    public void NodeEvent(Node node)
    {
        switch (node.nodeType)
        {
            case NodeType.Enemy :
                print("ENEMY APPEAR >:)");
                break;
        }
    }
}
