using UnityEngine;

public class ParryDestroyAfter : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 2);
    }
}
