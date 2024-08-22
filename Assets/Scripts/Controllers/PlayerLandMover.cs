using UnityEngine;

public class PlayerLandMover : LandMover
{
    [SerializeField] private PlayerInputScriptableObject playerInputData;
    [SerializeField] private Transform aimPosition;
    [SerializeField] private Weapon playerWeapon;
    [SerializeField] private StatsInfo statsInfo;

    private PlayerInput playerInput;

    public void Awake()
    {
        base.Awake();

        playerInput = new PlayerInput(playerInputData);

        if (playerWeapon)
        {
            playerWeapon.onThrowed = (Throwable throwable) => {
                //do something
            };

            playerWeapon.onHitedTarget = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) => {
                collider.GetComponent<EnemyLandMover>().Hurt(throwable.damage, dirHit, magHit);
            };

            playerWeapon.onHitedSomething = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) => {
                //do something
            };
        }
    }

    public void Start()
    {
        playerInput.EnableInputs();
        if (GameManager.instance) GameManager.instance.playerInput = playerInput;
    }

    public void Update()
    {
        if (playerInput.isThrowPressed()) playerWeapon.Attack();
        if (playerInput.isJumpPressed()) base.Jump();

        base.xdir = playerInput.GetMoveXDir();

        playerWeapon.SetAim(playerInput.GetAimDir());

        base.UpdateAnimation();
    }

    public void FixedUpdate()
    {
        base.UpdateMovement();
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        base.UpdateCollisionStay2D(col);
    }

    public void OnCollisionExit2D(Collision2D col) 
    {
        base.UpdateCollisionExit(col);
    }

    public void Hurt(float damage, Vector2 dirHit, float magHit)
    {
        base.Hurt(damage, dirHit, magHit);
        statsInfo.UpdateHealth(-damage);
    }
}
