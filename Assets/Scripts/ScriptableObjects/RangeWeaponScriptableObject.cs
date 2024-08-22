using UnityEngine;

[CreateAssetMenu(fileName = "new RangeWeaponData", menuName = "ScriptableObjects/Weapons/RangeWeaponData")]
public class RangeWeaponScriptableObject : ScriptableObject
{
    [Header("Info")]
    public float throwForce;

    [Header("Throwable")]
    public Throwable throwable;
    public float offset;

    [Header("TAG Info")]
    public string tagSelfThrower = "Player";
    public string tagThrowable = "Bullet";
    public string tagGround = "Ground";

    [Header("Trajectory Info")]
    public int maxPhysicsFrameIterations = 30;
}