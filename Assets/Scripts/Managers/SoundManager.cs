using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {
        get => _instance;
        private set => _instance = value;
    }

    public List<AudioClip> sceneAudioClipList;

    public float musicVolume {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            PlayerPrefs.SetFloat(musicPlayerPrefs, value);
            if (_musicSource != null) _musicSource.volume = value;
        }
    }

    public float sfxVolume
    {
        get => _sfxVolume;
        set
        {
            _sfxVolume = value;
            PlayerPrefs.SetFloat(sfxPlayerPrefs, value);
            sfxSourceList.ForEach(x => {
                if (x != null) x.volume = value;
            });
        }
    }

    private AudioListener _audioListener;
    private AudioSource _musicSource;
    private List<AudioSource> sfxSourceList = new List<AudioSource>();
    private static SoundManager _instance = null;
    private float _musicVolume;
    private float _sfxVolume;

    //@todo move to tag static class
    private string musicPlayerPrefs = "musicVolume";
    private string sfxPlayerPrefs = "sfxVolume";

    public void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        CreateAudioListenerComponent();

        _musicVolume = PlayerPrefs.HasKey(musicPlayerPrefs) ? PlayerPrefs.GetFloat(musicPlayerPrefs) : 0.7f;
        _sfxVolume = PlayerPrefs.HasKey(sfxPlayerPrefs) ? PlayerPrefs.GetFloat(sfxPlayerPrefs) : 0.5f;

        _musicSource = GetComponent<AudioSource>();
        _musicSource.volume = _musicVolume;
        _musicSource.enabled = true;

        _instance = this;
        if (GameManager.instance) GameManager.instance.soundManager = this;
        DontDestroyOnLoad(this);
        
    }

    //NEED CHANGE SFX MANAGER
    public AudioSource RegisterSfxSource(AudioSource sfxSource)
    {
        sfxSource.volume = sfxVolume;
        sfxSourceList.Add(sfxSource);

        return sfxSource;
    }

    public void UnregisterSfxSource(AudioSource sfxSource)
    {
        sfxSourceList.Remove(sfxSource);
    }
    public void NextSoundTrack()
    {
        //GOHORSE REFACTOR
        AudioClip nextMusicClip = sceneAudioClipList.First();
        _musicSource.clip = nextMusicClip;
        sceneAudioClipList.Remove(nextMusicClip);
        _musicSource.Play();
    }

    private void CreateAudioListenerComponent()
    {
        _audioListener = gameObject.AddComponent(typeof(AudioListener)) as AudioListener;
    }
}
