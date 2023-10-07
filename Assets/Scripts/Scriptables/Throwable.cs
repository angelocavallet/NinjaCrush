using System;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "new Throwable", menuName = "Throwable")]
public class Throwable : ScriptableObject
{
    [SerializeField] private GameObject prefabThrow;
    [SerializeField] private string prefabName;
    [SerializeField] private int throwForce;
    [SerializeField] private float fireRateSeconds;
    [SerializeField] private float damage;

    private int instanceNumber = 1;
    private Action<Collider2D,ThrowableBehaviour> onHitedSomething;

    public void ThrowNew(Transform originTransform, Vector2 dirVelocity)
    {
        float angle = Mathf.Atan2(dirVelocity.y, dirVelocity.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject throwedEntity = Instantiate(prefabThrow, originTransform.position, rotation);

        throwedEntity.name = prefabName + instanceNumber;
        throwedEntity.GetComponent<Rigidbody2D>().velocity = dirVelocity * throwForce;
        throwedEntity.GetComponent<ThrowableBehaviour>().SetOnHitedSomething(onHitedSomething);

        instanceNumber++;
    }

    public float GetFireRateSeconds()
    {
        return fireRateSeconds;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetOnHitedSomething(Action<Collider2D,ThrowableBehaviour> onHitedSomething)
    {
        this.onHitedSomething = onHitedSomething;
    }
}