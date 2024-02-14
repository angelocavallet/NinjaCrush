using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string namePrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwCooldown;

    [Header("Throwable")]
    [SerializeField] public Throwable throwable;

    [Header("TAG Info")]
    [SerializeField] private const string TAG_TARGET = "Enemy";
    [SerializeField] private const string TAG_SELF_THROWER = "Player";
    [SerializeField] private const string TAG_THROWABLE = "Bullet";

    public Action<Throwable> onThrowed { private get; set; }
    public Action<Collider2D, Throwable> onHitedTarget { private get; set; }
    public Action<Collider2D, Throwable> onHitedSomething { private get; set; }
    public Transform transform { private get; set; }

    private Vector2 aimDirection;
    private float nextShoot = 0f;
    private int instanceNumber = 1;

    public Weapon Clone()
    {
        return Instantiate(this);
    }

    public Weapon InstantiateCloneInsideTransform(Transform transform)
    {
        GameObject newWeaponInstance = Instantiate(weaponPrefab, transform.position, transform.rotation, transform);
        instanceNumber++;
        newWeaponInstance.name = $"{namePrefab} ({instanceNumber})";
        return newWeaponInstance.GetComponent<WeaponController>().weapon;
    }

    //@todo for mobile here need to change this to receive angle and decide angle in PlayerInput
    public void SetAim(Vector2 pointerPosition)
    {
        aimDirection = (pointerPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + 90f));
    }

    public void Throw()
    {
        if (Time.time < nextShoot) return;

        nextShoot = Time.time + throwCooldown;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Transform newTransform = transform;
        newTransform.rotation = rotation;
        Throwable newThrowable = throwable.InstantiateCloneAtTransform(transform);

        newThrowable.selfTag = TAG_THROWABLE;
        newThrowable.targetTag = TAG_TARGET;
        newThrowable.selfThrowerTag = TAG_SELF_THROWER;
        newThrowable.onThrowed = onThrowed;

        newThrowable.onHitedTarget = onHitedTarget;
        newThrowable.onHitedSomething = onHitedSomething;

        newThrowable.Throw(aimDirection * throwForce);
        newThrowable.PlayThrowAudioClip();

        instanceNumber++;
    }
}