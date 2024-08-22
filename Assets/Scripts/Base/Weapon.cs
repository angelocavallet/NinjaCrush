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

    protected float attackCooldownSeconds;
    protected Vector2 aimDirection;
    protected float lastAttackTime = -Mathf.Infinity;

    public virtual void Awake()
    {   
        tagTarget = weaponData.tagTarget;
        attackCooldownSeconds = weaponData.attackCooldownSeconds;
    }

    public virtual void Update()
    {

    }

    public virtual void SetAim(Vector2 aimPosition)
    {
        aimDirection = (aimPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + 90f));
    }

    public virtual void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldownSeconds)
        {
            lastAttackTime = Time.time;
        }
    }
}