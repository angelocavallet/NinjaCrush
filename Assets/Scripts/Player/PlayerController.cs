using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public const string TAG_GROUND = "Ground";

    [Header("Scriptable Objects")]
    [SerializeField] private LandMover playerLandMover;
    //[SerializeField] private Weapon playerWeapon;

    [Header("Inputs")]
    [SerializeField] private InputAction moveInput;
    [SerializeField] private InputAction jumpInput;
    [SerializeField] private InputAction aimInput;
    [SerializeField] private InputAction throwInput;

    private WeaponController playerWeaponController;

    public void Awake()
    {
        playerLandMover = playerLandMover.Clone();
        playerLandMover.SetRigidbody2D(GetComponent<Rigidbody2D>());
        playerLandMover.SetSpriteRenderer(GetComponent<SpriteRenderer>());
        playerLandMover.SetAnimator(GetComponent<Animator>());
        playerWeaponController = GetComponentInChildren<WeaponController>();
    }

    public void Start()
    {
        moveInput.Enable();
        jumpInput.Enable();
        aimInput.Enable();
        throwInput.Enable();
        playerLandMover.EnableDebug();
    }

    public void FixedUpdate()
    {
        playerLandMover.SetXDir(moveInput.ReadValue<float>());
        playerLandMover.UpdateMovement();
    }

    public void Update()
    {
        if (jumpInput.triggered) playerLandMover.Jump();
        if (throwInput.triggered) playerWeaponController.Shoot();
        playerLandMover.UpdateAnimation();

        playerWeaponController.SetAimWeapon(Camera.main.ScreenToWorldPoint(aimInput.ReadValue<Vector2>()));

    }

    void OnCollisionStay2D(Collision2D col)
    {
        playerLandMover.updateCollisionStay2D(col);
    }

    void OnCollisionExit2D(Collision2D col) 
    {
        playerLandMover.updateCollisionExit(col);
    }
}
