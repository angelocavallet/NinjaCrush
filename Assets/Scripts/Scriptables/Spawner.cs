using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "new SpawnManager", menuName = "ScriptableObjects/SpawnManager")]
public class SpawnManager : ScriptableObject
{
    [SerializeField] private string tagSpawnLocations;
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private string prefabName;
    [SerializeField] private int prefabPerWave;
    [SerializeField] private float progressionEachWave;
    [SerializeField] private float secondsPerWave;

    public Action spawnAction { private get; set; }
    private int instanceNumber = 1;
    private System.Random rand = new System.Random();
    private List<GameObject> spawnPointList = new List<GameObject>();
    private float nextSpawnExec = 0.0f;

    public SpawnManager Clone()
    {
        return Instantiate(this);
    }

    public void DiscoverSpawnPoints(GameObject spawnerGameObject)
    {
        spawnPointList = GameObject.FindGameObjectsWithTag(tagSpawnLocations).ToList<GameObject>();

        if (spawnPointList.Count == 0) throw new Exception($"Spawner -> No GameObject with ${tagSpawnLocations} tag was found :(");
    }

    public void UpdateSpawnManager()
    {
        if (Time.time < nextSpawnExec) return;
        
        nextSpawnExec += secondsPerWave;
        if (spawnAction == null) spawnAction = SpawnEntitiesAtDistribuitedPoints;

        spawnAction();
    }

    public void SpawnEntitiesAtRandomPoints()
    {
        for (int i = 0; i < prefabPerWave; i++)
        {

            GameObject spawnPoint = spawnPointList[rand.Next(0, spawnPointList.Count)];

            GameObject currentEntity = Instantiate(prefabToSpawn, spawnPoint.transform.position, Quaternion.identity);

            currentEntity.name = prefabName + instanceNumber;

            instanceNumber++;
        }
    }

    public void SpawnEntitiesAtDistribuitedPoints()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < prefabPerWave; i++)
        {
            GameObject spawnPoint = spawnPointList[currentSpawnPointIndex];

            GameObject currentEntity = Instantiate(prefabToSpawn, spawnPoint.transform.position, Quaternion.identity);

            currentEntity.name = prefabName + instanceNumber;

            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPointList.Count;

            instanceNumber++;
        }
    }
}