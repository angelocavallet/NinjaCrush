using UnityEngine;

public class SoundManager
{
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

    private float _musicVolume;
    private float _masterVolume;
    private AudioSource musicSource;
    private AudioClip baseMusicAudioClip;

    public SoundManager(SoundManagerScriptableObject soundManagerData, AudioSource someMusicSource)
    {
        musicSource = someMusicSource;
        baseMusicAudioClip = soundManagerData.baseMusicClip;
        masterVolume = soundManagerData.masterVolume;
        musicVolume = soundManagerData.musicVolume;

        if (!musicSource.clip || soundManagerData.overrideAudioSourceComponentClip) musicSource.clip = baseMusicAudioClip;

        musicSource.loop = true;
        musicSource.Play();
    }

    public void StartOtherMusicClip(AudioClip newMusicAudioClip)
    {
        musicSource.clip = newMusicAudioClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StartBaseMusicClip()
    {
        musicSource.clip = baseMusicAudioClip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
