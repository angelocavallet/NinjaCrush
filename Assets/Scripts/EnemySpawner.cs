using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public SpawnManagerSO enemySpawner;

    void Start()
    {
        enemySpawner.SpawnEntitiesAtDistribuitedPoints();
    }
}