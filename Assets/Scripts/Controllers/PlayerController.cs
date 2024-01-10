using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LandMover playerLandMover;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Weapon playerWeapon;
    [SerializeField] private Transform aimPosition;

    public void Awake()
    {
        if (GameManager.instance) GameManager.instance.playerInput = playerInput;

        playerLandMover = playerLandMover.Clone();
        playerLandMover.rigidbody2D = GetComponent<Rigidbody2D>();
        playerLandMover.spriteRenderer = GetComponent<SpriteRenderer>();
        playerLandMover.animator = GetComponent<Animator>();

        playerWeapon = playerWeapon.InstantiateCloneInsideTransform(aimPosition);

        playerWeapon.onThrowed = (Throwable throwable) => {
            //do something
        };

        playerWeapon.onHitedTarget = (Collider2D collider, Throwable throwable) => {
            collider.GetComponent<EnemyController>().Hurt(throwable.damage);
        };

        playerWeapon.onHitedSomething = (Collider2D collider, Throwable throwable) => {
            //do something
        };
    }

    public void Start()
    {
        playerInput.EnableInputs();
    }

    public void FixedUpdate()
    {
        if (playerInput.isThrowPressed()) playerWeapon.Throw();
        if (playerInput.isJumpPressed()) playerLandMover.Jump();

        playerLandMover.xdir = playerInput.GetMoveXDir();
        playerLandMover.UpdateMovement();
    }

    public void Update()
    {
        //@todo: future mobile port problem here, need to pass just angle received by playerInput method
        playerWeapon.SetAim(Camera.main.ScreenToWorldPoint(playerInput.GetAimDir()));

        playerLandMover.UpdateAnimation();
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        playerLandMover.UpdateCollisionStay2D(col);
    }

    public void OnCollisionExit2D(Collision2D col) 
    {
        playerLandMover.UpdateCollisionExit(col);
    }
}
