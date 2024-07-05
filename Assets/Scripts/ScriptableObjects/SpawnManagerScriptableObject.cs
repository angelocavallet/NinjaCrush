using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SpawnManagerData", menuName = "ScriptableObjects/GameManager/Level/SpawnManagerData")]
public class SpawnManagerScriptableObject : ScriptableObject
{
    public string tagSpawnLocations;
    public GameObject prefabToSpawn;
    public string prefabName;
    public int prefabPerWave;
    public float progressionEachWave;
    public float secondsPerWave;

    public Action spawnAction { private get; set; }
    private int instanceNumber = 1;
    private System.Random rand = new System.Random();
    private List<GameObject> spawnPointList = new List<GameObject>();
    private float nextSpawnExec = 0.0f;

}