using System;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerScriptableObject gameManagerData;
    [SerializeField] private AudioSource musicAudioSource;

    public static GameManager instance
    {
        get => _instance;
        private set => _instance = value;
    }

    public float writeDialogLetterEverySeconds 
    {
        get => gameManagerData.writeDialogLetterEverySeconds;
    }

    public SceneLoaderManager sceneLoaderManager
    {
        get => _sceneLoaderManager;
        private set => _sceneLoaderManager = value;
    }

    public SaveGameManager saveGameManager
    {
        get => _saveGameManager;
        private set => _saveGameManager = value;
    }

    public SoundManager soundManager
    {
       get => _soundManager;  
       private set => _soundManager = value;
    }

    public PlayerInput playerInput
    {
        get => _playerInput;
        set
        {
            _playerInput = value;
        }
    }

    public bool fullScreen
    {
        get => Screen.fullScreen;
        set => Screen.fullScreen = value;
    }

    public bool isPaused
    {
        get => _isPaused;
        private set
        {
            _isPaused = value;
        }
    }

    private static GameManager _instance = null;

    private SaveGameManager _saveGameManager;
    private SceneLoaderManager _sceneLoaderManager;
    private SoundManager _soundManager;
    private PlayerInput _playerInput;
    private bool _isPaused;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;

            LoadManagers();
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("Only a single GameManager instance must exists");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(sceneLoaderManager.LoadStartSceneAsync());
    }

    public void Continue()
    {
        isPaused = false;
        if (playerInput != null) playerInput.EnableInputs();
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        isPaused = true;
        if (playerInput != null) playerInput.DisableInputs();
        Time.timeScale = 0;
    }

    private void LoadManagers()
    {
        saveGameManager = new SaveGameManager();
        playerInput = new PlayerInput(gameManagerData.playerInputData);
        soundManager = new SoundManager(gameManagerData.soundManagerData, musicAudioSource);
        sceneLoaderManager = new SceneLoaderManager(gameManagerData.sceneLoaderManagerData);

        if (PlayerInput.instance != null) playerInput = PlayerInput.instance;
    }
}
