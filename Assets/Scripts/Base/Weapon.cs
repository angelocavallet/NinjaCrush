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

    private Renderer rend;
    private AudioSource audioSrc;

    public virtual void Awake()
    {
        rend = GetComponent<Renderer>();
        tagTarget = weaponData.tagTarget;
        attackCooldownSeconds = weaponData.attackCooldownSeconds;
    }

    public virtual void Update()
    {
        if (!rend.enabled && Time.time >= lastAttackTime + attackCooldownSeconds)
        {
            ToggleWeapon(true);
        }
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

            ToggleWeapon(false);
        }
    }

    private void ToggleWeapon(bool active)
    {
        rend.enabled = active;

        if (audioSrc)
        {
            if (active)
            {
                audioSrc.Play();
            }
            else
            {
                audioSrc.Stop();
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(active);
        }
    }
}