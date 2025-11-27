using System.Collections.Generic;
using UnityEngine;

public class CloudsMove : MonoBehaviour
{
    public List<GameObject> clouds;
    public float speed = 1f;
    public float resetPositionX = -10f;   // left boundary (off-screen)
    public float startPositionX = 10f;
    private void Update()
    {
        foreach (GameObject cloud in clouds)
        {
            if (cloud == null) continue;

            // Move left
            cloud.transform.position += Vector3.left * speed * Time.deltaTime;

            // If it goes off the left side, reset to the right
            if (cloud.transform.position.x <= resetPositionX)
            {
                Vector3 newPos = cloud.transform.position;
                newPos.x = startPositionX;
                cloud.transform.position = newPos;
            }
        }
    }
}
