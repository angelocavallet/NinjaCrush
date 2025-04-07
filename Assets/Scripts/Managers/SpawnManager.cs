using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class SpawnManager : NetworkBehaviour 
{
    public string tagSpawnPoints;

    private List<GameObject> spawnPointList = new List<GameObject>();
    private List<LevelRoundData> levelRoundDataList;
    private int instanceNumber = 1;
    private float nextSpawnExec = 0f;
    private float secondsPerWave = 0f;
    private int actualLevelRoundData = 0;
    private int actualLevelWaveData = 0;
    private bool loaded = false;

    public void Awake()
    {
        if (!IsHost) return;

        spawnPointList = GameObject.FindGameObjectsWithTag(tagSpawnPoints).ToList<GameObject>();

        if (spawnPointList.Count == 0) throw new Exception($"Spawner -> No GameObject with ${tagSpawnPoints} tag was found :(");
    }

    public void Update()
    {
        if (!IsHost) return;
        if (!loaded) return;
        if (Time.time < nextSpawnExec) return;

        SpawnEntitiesAtDistribuitedPoints();
    }

    public void LoadLevelRoundData(LevelManagerScriptableObject levelManagerData)
    {
        levelRoundDataList = levelManagerData.roundsList;
        secondsPerWave = levelManagerData.levelTimeSeconds / levelRoundDataList.SelectMany(round => round.waveList).Count();

        Debug.Log($"Seconds Per Wave: {secondsPerWave}");

        SpawnEntitiesAtDistribuitedPoints();
        loaded = true;
    }

    private void SpawnEntitiesAtDistribuitedPoints()
    {
        if (!spawnPointList.Any()) return;
        if (actualLevelRoundData >= levelRoundDataList.Count) return;
        if (actualLevelWaveData >= levelRoundDataList[actualLevelRoundData].waveList.Count) return;

        nextSpawnExec = Time.time + secondsPerWave;

        GameObject spawnPrefab = levelRoundDataList[actualLevelRoundData].waveList[actualLevelWaveData].prefabToSpawn;
        int numberOfEnemies = levelRoundDataList[actualLevelRoundData].waveList[actualLevelWaveData].numberOfEnemies;
        int currentSpawnPointIndex = 0;

        Debug.Log($"Round {actualLevelRoundData} Wave {actualLevelWaveData} of {numberOfEnemies} {spawnPrefab.name}");

        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject spawnPoint = spawnPointList[currentSpawnPointIndex];

            GameObject currentEntity = Instantiate(spawnPrefab, spawnPoint.transform);

            currentEntity.name = spawnPrefab.name + instanceNumber;

            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPointList.Count;

            instanceNumber++;
        }

        actualLevelWaveData++;

        if (actualLevelWaveData >= levelRoundDataList[actualLevelRoundData].waveList.Count)
        {
            actualLevelWaveData = 0;
            actualLevelRoundData++;
        }
        Debug.Log($"Toal Number of Enemies: {instanceNumber}");
    }
    /*
    public void SpawnEntitiesAtRandomPoints()
    {
        for (int i = 0; i < prefabPerWave; i++)
        {

            GameObject spawnPoint = spawnPointList[rand.Next(0, spawnPointList.Count)];

            GameObject currentEntity = Instantiate(spawnPrefab, spawnPoint.transform);

            currentEntity.name = spawnPrefab.name + instanceNumber;

            instanceNumber++;
        }
    }
    */
}