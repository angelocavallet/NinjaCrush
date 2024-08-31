using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelManagerScriptableObject levelManagerData;

    public SpawnManager spawnManager;
    public TextMeshProUGUI scoreDisplayText;
    public GameObject canvas;

    public float levelTimeSeconds
    {
        get => levelManagerData.levelTimeSeconds;
        set => levelManagerData.levelTimeSeconds = value;
    }

    public float timeLeft
    {
        get => _timeLeft;
        set => _timeLeft = value;
    }

    public float score
    {
        get => _score;
    }

    private float _timeLeft;
    private float _score;

    public void Awake()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.Continue();
            GameManager.instance.soundManager.StartOtherMusicClip(levelManagerData.levelSoundTrack);
        }
    }

    public void Start()
    {
        if (spawnManager)
        {
            spawnManager.LoadLevelRoundData(levelManagerData);
        }
    }

}
