using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public SpawnManager enemySpawnManager;

    public void Start()
    {
        enemySpawnManager = enemySpawnManager.Clone();
        enemySpawnManager.DiscoverSpawnPoints(gameObject);
        enemySpawnManager.spawnAction = enemySpawnManager.SpawnEntitiesAtRandomPoints;
    }

    public void Update()
    {
        enemySpawnManager.UpdateSpawnManager();
    }

}