using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LandMover playerLandMover;
    [SerializeField] private Transform aimPosition;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Weapon playerWeapon;
    [SerializeField] public StatsInfo statsInfo;

    public void Awake()
    {
        playerLandMover = playerLandMover.Clone();

        playerLandMover.transform = transform;
        playerLandMover.rigidbody2D = GetComponent<Rigidbody2D>();
        playerLandMover.spriteRenderer = GetComponent<SpriteRenderer>();
        playerLandMover.animator = GetComponent<Animator>();

        statsInfo = statsInfo.Clone(transform, playerLandMover.health);
        playerWeapon = playerWeapon.Clone(aimPosition);

        
        playerWeapon.onThrowed = (Throwable throwable) => {
            //do something
        };

        playerWeapon.onHitedTarget = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) => {
            collider.GetComponent<EnemyController>().Hurt(throwable.damage, dirHit, magHit);
        };

        playerWeapon.onHitedSomething = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) => {
            //do something
        };
        
    }

    public void Start()
    {
        playerInput.EnableInputs();
        if (GameManager.instance) GameManager.instance.playerInput = playerInput;
    }

    public void Update()
    {
        if (playerInput.isThrowPressed()) playerWeapon.Throw();
        if (playerInput.isJumpPressed()) playerLandMover.Jump();

        playerLandMover.xdir = playerInput.GetMoveXDir();

        //@todo: future mobile port problem here, need to pass just angle received by playerInput method
        playerWeapon.SetAim(Camera.main.ScreenToWorldPoint(playerInput.GetAimDir()));

        playerLandMover.UpdateAnimation();
    }

    public void FixedUpdate()
    {
        playerLandMover.UpdateMovement();
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        playerLandMover.UpdateCollisionStay2D(col);
    }

    public void OnCollisionExit2D(Collision2D col) 
    {
        playerLandMover.UpdateCollisionExit(col);
    }

    public void Hurt(float damage, Vector2 dirHit, float magHit)
    {
        playerLandMover.Hurt(damage, dirHit, magHit);
        statsInfo.UpdateHealth(-damage);
    }
}
