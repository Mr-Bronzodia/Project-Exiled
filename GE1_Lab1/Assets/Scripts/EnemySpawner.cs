using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.5f, 10f)]
    public float quantityMultiplier = 1;
    public int level;
    public List<GameObject> enemyPrefabs;


    private const int baseRate = 3;

    void Start()
    {

        SpawnEnemies();
    }

    
    public void SpawnEnemies()
    {
        for (int i = 0; i < (int)(baseRate * quantityMultiplier); i++)
        {
            int rate = (int)(baseRate * quantityMultiplier);
            Vector3 enemySpawnLocation = new Vector3(gameObject.transform.position.x + Random.Range(-rate, rate), gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-rate, rate));
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], enemySpawnLocation, Quaternion.Euler(0, Random.Range(0, 180), 0));
            enemy.GetComponent<Character>().level = level;
        }
    }

}
