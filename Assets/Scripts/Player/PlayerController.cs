using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public const string TAG_GROUND = "Ground"; 

    [SerializeField] private LandMover playerLandMover;
    [SerializeField] private InputAction moveInput;
    [SerializeField] private InputAction jumpInput;

    public void Awake()
    {
        playerLandMover = playerLandMover.Clone();
        playerLandMover.SetRigidbody2D(GetComponent<Rigidbody2D>());
        playerLandMover.SetSpriteRenderer(GetComponent<SpriteRenderer>());
        playerLandMover.SetAnimator(GetComponent<Animator>());
    }

    public void Start()
    {
        moveInput.Enable();
        jumpInput.Enable();
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
        playerLandMover.UpdateAnimation();
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
