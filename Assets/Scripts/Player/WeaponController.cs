using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public void SetAimWeapon(Vector2 pointerPosition)
    {
        Vector2 direction = (pointerPosition - (Vector2)transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var offset = 90f;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
