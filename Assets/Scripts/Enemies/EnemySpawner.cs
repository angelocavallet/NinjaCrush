using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Spawner enemySpawner;

    public void Start()
    {
        enemySpawner.DiscoverSpawnPoints(gameObject);
        StartCoroutine("SpawnEnemy");
    }


    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            enemySpawner.SpawnEntitiesAtRandomPoints();
            yield return new WaitForSeconds(enemySpawner.GetSecondsBetweenWaves());
        }
    }
}