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

    public List<WaveData> listaFases = new List<WaveData>();
}