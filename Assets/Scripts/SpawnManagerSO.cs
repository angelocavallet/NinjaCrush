using UnityEngine;
using UnityEngine.Jobs;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerSO", order = 1)]
public class SpawnManagerSO : ScriptableObject
{
    [SerializeField] private GameObject entityToSpawn;
    [SerializeField] private string prefabName;
    [SerializeField] private int numberOfPrefabsToCreate;
    [SerializeField] private Vector2[] spawnPointList;

    private int instanceNumber = 1;

    public void SpawnEntitiesAtRandomPoints()
    {
        for (int i = 0; i < numberOfPrefabsToCreate; i++)
        {
            Vector2 spawnPoint = spawnPointList[Random.Range(0, spawnPointList.Length -1)];

            GameObject currentEntity = Instantiate(entityToSpawn, spawnPoint, Quaternion.identity);

            currentEntity.name = prefabName + instanceNumber;

            instanceNumber++;
        }
    }

    public void SpawnEntitiesAtDistribuitedPoints()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < numberOfPrefabsToCreate; i++)
        {
            Vector2 spawnPoint = spawnPointList[currentSpawnPointIndex];

            GameObject currentEntity = Instantiate(entityToSpawn, spawnPoint, Quaternion.identity);

            currentEntity.name = prefabName + instanceNumber;

            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPointList.Length;

            instanceNumber++;
        }
    }
}