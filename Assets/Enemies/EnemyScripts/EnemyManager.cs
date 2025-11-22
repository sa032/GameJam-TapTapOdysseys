using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject[] enemiesToSpawn;
    public Transform ObjectContainer;
    private void Start()
    {
        instance = this;
        //SpawnEnemies();
    }
    private void Update()
    {
        CleanList();
    }
    public void SpawnEnemies(List<GameObject> Enemies)
    {
        float startX = transform.position.x; // starting x position
        float y = 0f;      // fixed y position
        float z = 0f;      // fixed z position

        for (int i = 0; i < Enemies.Count; i++)
        {
            GameObject enemyPrefab = Enemies[i];
            if (enemyPrefab != null)
            {
                // Calculate position for this enemy
                Vector3 spawnPos = new Vector3(startX + i*2, y, z);

                // Instantiate enemy
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                newEnemy.transform.parent = ObjectContainer;

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
    public List<GameObject> GetAllEnemies()
    {
        CleanList();
        return enemies;
    }
}
