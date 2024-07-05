using UnityEngine;

[CreateAssetMenu(fileName = "new ThrowableData", menuName = "ScriptableObjects/Ammo/ThrowableData")]
public class ThrowableScriptableObject : ScriptableObject
{
    [Header("General Info")]
    public string namePrefab;
    public GameObject throwablePrefab;
    public float mass;
    public float damage;
    public float lifeTimeSeconds;

    //@todo: Need Effect base class
    [Header("Effect Prefabs")]
    public GameObject throwEffectPrefab;
    public GameObject hitTargetEffectPrefab;
    public GameObject hitOtherEffectPrefab;

    //@todo: Need Effect base class
    [Header("Audio Clips")]
    public AudioClip throwAudioClip;
    public AudioClip hitTargetAudioClip;
    public AudioClip hitOtherAudioClip;
}