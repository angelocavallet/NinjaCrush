using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new LevelManagerData", menuName = "ScriptableObjects/GameManager/LevelManagerData")]
public class LevelManagerScriptableObject : ScriptableObject
{
    public string levelSceneName;

    public string levelDisplayName;

    public Sprite levelDisplaySprite;

    public float levelTimeSeconds;

    public AudioClip levelSoundTrack;

    public List<LevelRoundData> roundsList = new List<LevelRoundData>();

}

[System.Serializable]
public class LevelRoundData
{
    public string name;

    public List<LevelWaveData> waveList = new List<LevelWaveData>();
}

[System.Serializable]
public class LevelWaveData
{
    public int numberOfEnemies;
    public GameObject prefabToSpawn;
} 