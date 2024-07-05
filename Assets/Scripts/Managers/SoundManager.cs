using System;
using UnityEngine;

public class SoundManager
{
    public static SoundManager instance {
        get => _instance;
        private set => _instance = value;
    }
    public float masterVolume
    {
        get => _masterVolume;
        set
        {
            AudioListener.volume = value;
            _masterVolume = value;
        }
    }

    public float musicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            if (musicSource != null) musicSource.volume = value;
        }
    }

    private static SoundManager _instance = null;
    private float _musicVolume;
    private float _masterVolume;
    private AudioSource musicSource;
    private AudioClip baseMusicAudioClip;

    public SoundManager(SoundManagerScriptableObject soundManagerData, AudioSource someMusicSource)
    {
        if (instance != null) throw new Exception("Only a single SoundManager instance must exists");

        musicSource = someMusicSource;
        baseMusicAudioClip = soundManagerData.baseMusicClip;
        masterVolume = soundManagerData.masterVolume;
        musicVolume = soundManagerData.musicVolume;

        if (!musicSource.clip || soundManagerData.overrideAudioSourceComponentClip) musicSource.clip = baseMusicAudioClip;

        musicSource.Play();

        instance = this;
    }

    public void StartOtherMusicClip(AudioClip newMusicAudioClip)
    {
        musicSource.clip = newMusicAudioClip;
    }

    public void StartBaseMusicClip()
    {
        musicSource.clip = baseMusicAudioClip;
    }
}
