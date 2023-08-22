using System.ComponentModel;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Throwable throwable;
    private Vector2 aimDirection;
    private float nextFire = 0f;

    public void Awake()
    {
        throwable.SetOnHitedSomething((Collider2D collider, ThrowableBehaviour throwableBehaviour) => {
            if (!collider.gameObject.CompareTag(throwableBehaviour.tag))
            {
                throwableBehaviour.SetIsHited(true);
                throwableBehaviour.GetEffectGameObject().SetActive(false);
                throwableBehaviour.GetRigidBody2D().velocity = Vector3.zero;
                throwableBehaviour.GetRigidBody2D().isKinematic = true;
                throwableBehaviour.transform.SetParent(collider.gameObject.transform);
            }
        });
    }

    public void SetAimWeapon(Vector2 pointerPosition)
    {
        aimDirection = (pointerPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + 90f));
    }

    public void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + throwable.GetFireRateSeconds();
            throwable.ThrowNew(transform, aimDirection);
        }
    }
}
