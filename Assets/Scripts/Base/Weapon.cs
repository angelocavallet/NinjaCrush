using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    public Action<Throwable> onThrowed { protected get; set; }
    public Action<Collider2D, Throwable, Vector2, float> onHitedTarget { protected get; set; }
    public Action<Collider2D, Throwable, Vector2, float> onHitedSomething { protected get; set; }
    public LineRenderer lineRenderer { private get; set; }

    public string tagTarget;
    public float attackCooldownSeconds;

    public void Awake()
    {   
        tagTarget = weaponData.tagTarget;
        attackCooldownSeconds = weaponData.attackCooldownSeconds;
    }

    public void StartAttack()
    {
    }
}