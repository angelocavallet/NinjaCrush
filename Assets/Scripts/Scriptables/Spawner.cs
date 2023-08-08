using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Spawner", menuName = "Spawner")]
public class Spawner: ScriptableObject
{
    [SerializeField] private GameObject entityToSpawn;
    [SerializeField] private string prefabName;
    [SerializeField] private int qtdPrefabsOnWaves;
    [SerializeField] private int secondsBetweenWaves;

    private int instanceNumber = 1;
    private List<Vector2> spawnPointList = new List<Vector2>();

    public void DiscoverSpawnPoints(GameObject spawnerGameObject)
    {
        if (spawnerGameObject.transform.childCount == 0) {
            throw new Exception($"The GameObject ${spawnerGameObject.name} has no children gameObjects to load as SpawnPoint");
        } 

        for (int i = 0; i < spawnerGameObject.transform.childCount; i++)
        {
            spawnPointList.Add(spawnerGameObject.transform.GetChild(i).transform.position);

        }

    }

    public void SpawnEntitiesAtRandomPoints()
    {
        System.Random rand = new System.Random();

        for (int i = 0; i < qtdPrefabsOnWaves; i++)
        {

            Vector2 spawnPoint = spawnPointList[rand.Next(0, spawnPointList.Count)];

            GameObject currentEntity = Instantiate(entityToSpawn, spawnPoint, Quaternion.identity);

            currentEntity.name = prefabName + instanceNumber;

            instanceNumber++;
        }
    }

    public void SpawnEntitiesAtDistribuitedPoints()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < qtdPrefabsOnWaves; i++)
        {
            Vector2 spawnPoint = spawnPointList[currentSpawnPointIndex];

            GameObject currentEntity = Instantiate(entityToSpawn, spawnPoint, Quaternion.identity);

            currentEntity.name = prefabName + instanceNumber;

            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPointList.Count;

            instanceNumber++;
        }
    }

    public int GetSecondsBetweenWaves()
    {
        return secondsBetweenWaves;
    }
}