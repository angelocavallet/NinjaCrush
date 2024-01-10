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
            if (musicSource != null) musicSource.volume = value;
        }
    }

    public float sfxVolume
    {
        get => _sfxVolume;
        set
        {
            _sfxVolume = value;
            PlayerPrefs.SetFloat(sfxPlayerPrefs, value);
            sfxSourceList.ForEach(x => x.volume = value);
        }
    }

    private AudioSource musicSource;
    private List<AudioSource> sfxSourceList = new List<AudioSource>();
    private static SoundManager _instance;
    private float _musicVolume;
    private float _sfxVolume;

    //@todo move to tag static class
    private string musicPlayerPrefs = "musicVolume";
    private string sfxPlayerPrefs = "sfxVolume";

    public void Awake()
    {
        if (instance == null)
        {
            _musicVolume = PlayerPrefs.HasKey(musicPlayerPrefs) ? PlayerPrefs.GetFloat(musicPlayerPrefs) : 0.7f;
            _sfxVolume = PlayerPrefs.HasKey(sfxPlayerPrefs) ? PlayerPrefs.GetFloat(sfxPlayerPrefs) : 0.5f;

            musicSource = GetComponent<AudioSource>();
            musicSource.volume = _musicVolume;

            instance = this;
            if (GameManager.instance) GameManager.instance.soundManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

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
        musicSource.clip = nextMusicClip;
        sceneAudioClipList.Remove(nextMusicClip);
        musicSource.Play();
    }
}
