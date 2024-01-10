using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get => _instance;
        private set => _instance = value;
    }

    public SoundManager soundManager
    {
        get => _soundManager;
        set
        {
            _soundManager = value;
        }
    }

    public PlayerInput playerInput
    {
        get => _playerInput;
        set
        {
            _playerInput = value;
        }
    }

    public Boolean isPaused
    {
        get => _isPaused;
        private set
        {
            _isPaused = value;
        }
    }

    private static GameManager _instance;
    private static SoundManager _soundManager;
    private static PlayerInput _playerInput;
    private static Boolean _isPaused;

    public void Awake()
    {
        if (instance == null)
        {
            LoadActiveManagers();

            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Continue()
    {
        isPaused = false;
        playerInput.EnableInputs();
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        isPaused = true;
        playerInput.DisableInputs();
        Time.timeScale = 0;
    }

    private void LoadActiveManagers()
    {
        if (SoundManager.instance) soundManager = SoundManager.instance;
        if (PlayerInput.instance) playerInput = PlayerInput.instance;
    }
}
