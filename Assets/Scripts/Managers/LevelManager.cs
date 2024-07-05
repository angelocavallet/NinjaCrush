using TMPro;
using UnityEngine;

public class LevelManager
{
    public LevelManagerScriptableObject levelManagerData;

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

    public void Start()
    {
        GameManager.instance.Continue();
        SoundManager.instance.StartOtherMusicClip(levelManagerData.levelSoundTrack);
    }

}
