using UnityEngine;

[CreateAssetMenu(fileName = "new WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponScriptableObject : ScriptableObject
{
    [Header("Info")]
    public float attackCooldownSeconds;

    [Header("TAG Info")]
    public string tagTarget = "Enemy";
}