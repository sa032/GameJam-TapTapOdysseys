using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject[] enemiesToSpawn;
    private void Start()
    {
        SpawnEnemies();
    }
    private void Update()
    {
        CleanList();
    }
    public void SpawnEnemies()
    {
        float startX = transform.position.x; // starting x position
        float y = 0f;      // fixed y position
        float z = 0f;      // fixed z position

        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            GameObject enemyPrefab = enemiesToSpawn[i];
            if (enemyPrefab != null)
            {
                // Calculate position for this enemy
                Vector3 spawnPos = new Vector3(startX + i*2, y, z);

                // Instantiate enemy
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                // Add to list
                enemies.Add(newEnemy);
            }
        }
    }
    public void CleanList()
    {
        enemies.RemoveAll(e => e == null); // Remove dead/destroyed enemies
    }

    public GameObject GetFirstEnemy()
    {
        if (enemies.Count > 0)
            return enemies[0];

        return null;
    }
}
