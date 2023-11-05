using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public Weapon weapon;

    public void Awake()
    {
        weapon = weapon.Clone();
        weapon.transform = transform;
    }
}
