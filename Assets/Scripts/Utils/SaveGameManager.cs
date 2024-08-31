using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;

public class SaveGameManager
{
    public GameData gameData { get; private set; }

    private static string gameDataPath = Application.persistentDataPath + "/GameData.json";

    public SaveGameManager() {
        LoadGame();
    }

    public void NewSaveGame()
    {
        gameData.saveDataList.Add(new SaveData());
        writeSaveFile();
    }

    private void writeSaveFile()
    {
        File.WriteAllText(gameDataPath, JsonUtility.ToJson(gameData));
    }
    private void LoadGame()
    {
        try
        {
            string gameDataJson = File.ReadAllText(gameDataPath);
            gameData = JsonUtility.FromJson<GameData>(gameDataJson);

        }
        catch (FileNotFoundException)
        {
            gameData = new GameData();
            writeSaveFile();
        }
    }
}

[System.Serializable]
public class GameData
{
    public DateTime startDate = DateTime.Now;
    public float masterVolume = 1.0f;
    public float musicVolume = 1.0f;
    public List<SaveData> saveDataList = new List<SaveData>();
}

[System.Serializable]
public class SaveData
{
    public DateTime startDate = DateTime.Now;
    public string name = "";
    public PlayerData playerData = new PlayerData();
    public List<LevelData> levelDataList = new List<LevelData>();
}

[System.Serializable]
public class PlayerData
{
    public string name = "";

    public int level = 0;
    public BigInteger experience = 0L;

    public float healthValue = 0f;
    public float strengthValue = 0f;
    public float intelligenceValue = 0f;
    public float charismaValue = 0f;
    public float criticalValue = 0f;
    public float regenerationValue = 0f;

    public List<Weapon> weaponsList = new List<Weapon>();
}

[System.Serializable]
public class LevelData
{
    public string name;
    public List<WaveData> faseDataList = new List<WaveData>();
}

[System.Serializable]
public class WaveData
{
    public string name;
    public float score = 0;
    public int numberEnemiesKilled;
    public bool cleared = false;
}

