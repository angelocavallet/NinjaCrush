using UnityEngine;

[CreateAssetMenu(fileName = "new SoundManagerData", menuName = "ScriptableObjects/GameManager/SoundManagerData")]
public class SoundManagerScriptableObject : ScriptableObject
{
    public float masterVolume;
    public float musicVolume;
    public AudioClip baseMusicClip;
    public bool overrideAudioSourceComponentClip = false;
}