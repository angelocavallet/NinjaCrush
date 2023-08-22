using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Throwable", menuName = "Throwable")]
public class Throwable : ScriptableObject
{
    [SerializeField] private GameObject prefabThrow;
    [SerializeField] private string prefabName;
    [SerializeField] private int throwForce;
    [SerializeField] private float fireRateSeconds;

    private int instanceNumber = 1;
    private Action<Collider2D,ThrowableBehaviour> onHitedSomething;

    public void ThrowNew(Transform originTransform, Vector2 dirVelocity)
    {
        GameObject currentEntity = Instantiate(prefabThrow, originTransform.position, originTransform.rotation);
        currentEntity.name = prefabName + instanceNumber;
        currentEntity.GetComponent<Rigidbody2D>().velocity = dirVelocity * throwForce;
        currentEntity.GetComponent<ThrowableBehaviour>().SetOnHitedSomething(onHitedSomething);

        instanceNumber++;
    }

    public float GetFireRateSeconds()
    {
        return fireRateSeconds;
    }

    public void SetOnHitedSomething(Action<Collider2D,ThrowableBehaviour> onHitedSomething)
    {
        this.onHitedSomething = onHitedSomething;
    }
}