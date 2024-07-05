using UnityEngine;

[CreateAssetMenu(fileName = "new StatsInfoData", menuName = "ScriptableObjects/StatsInfoData")]
public class StatsInfoScriptableObject : ScriptableObject
{
    [Header("Info")]
    public float verticalVelocity;
    public float fadeVelocity;
    public float secondsToFade;

    [Header("StatsText Prefabs")]
    public GameObject statsTextPrefab;
}
