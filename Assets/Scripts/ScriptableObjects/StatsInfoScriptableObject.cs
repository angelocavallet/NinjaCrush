using UnityEngine;

[CreateAssetMenu(fileName = "new StatsInfoData", menuName = "ScriptableObjects/StatsInfoData")]
public class StatsInfoScriptableObject : ScriptableObject
{
    [Header("StatsInfoText Prefabs")]
    public GameObject DamageStatsInfoTextPrefab;
}
