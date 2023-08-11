using UnityEngine;

[CreateAssetMenu(fileName = "new Throwable", menuName = "Throwable")]
public class Throwable : ScriptableObject
{
    [SerializeField] private GameObject prefabThrow;
    [SerializeField] private string prefabName;
    [SerializeField] private int throwForce;
    [SerializeField] private float fireRateSeconds;

    private int instanceNumber = 1;

    public void ThrowNew(Transform originTransform, Vector2 dirVelocity)
    {
        GameObject currentEntity = Instantiate(prefabThrow, originTransform.position, originTransform.rotation);
        currentEntity.name = prefabName + instanceNumber;
        currentEntity.GetComponent<Rigidbody2D>().velocity = dirVelocity * throwForce;

        instanceNumber++;
    }

    public float GetFireRateSeconds()
    {
        return fireRateSeconds;
    }
}