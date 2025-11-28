using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject summon;
    public void SpawnSummon()
    {
        Instantiate(summon, transform.position, transform.rotation);
    }
}
