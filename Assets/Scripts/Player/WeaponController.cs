using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Throwable throwable;
    private Vector2 aimDirection;
    private float nextFire = 0f;

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
