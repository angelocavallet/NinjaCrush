using UnityEngine;

[CreateAssetMenu(fileName = "new StatsInfoTextData", menuName = "ScriptableObjects/StatsInfoTextData")]
public class StatsInfoTextScriptableObject : ScriptableObject
{
    [Header("Info")]
    public float verticalVelocity;
    public float fadeVelocity;
    public float secondsToFade;
}
