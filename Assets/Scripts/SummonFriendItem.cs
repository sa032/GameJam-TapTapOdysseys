using Map;
using UnityEngine;

public class SummonFriendItem : MonoBehaviour
{
    public GameObject[] Friends;
    public Vector3 Origin = new Vector3(-4.44f,-1.15f,0);
    
    void Awake()
    {
        GameObject FriendClone = Instantiate(Friends[MapManager.Instance.CurrentFloor]);
        FriendClone.transform.position = Origin + new Vector3(
            Random.Range(-1.5f,4.5f),
            Random.Range(-0.2f,1.1f),
            0
        );
        GameObject ParentSummon = GameObject.FindGameObjectWithTag("ParentSummon");
        FriendClone.transform.parent = ParentSummon.transform;
    }
}
