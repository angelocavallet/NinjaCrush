using UnityEngine;

public class PlayerLandMover : LandMover
{
    [SerializeField] private PlayerInputScriptableObject playerInputData;
    [SerializeField] private Transform aimPosition;

    private PlayerInput playerInput;

    public override void Awake()
    {
        base.Awake();

        playerInput = new PlayerInput(playerInputData);

        if (weapon)
        {
            weapon.onThrowed = (Throwable throwable) => {
                //do something
            };

            weapon.onHitedTarget = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) => {
                collider.GetComponent<EnemyLandMover>().Hurt(throwable.damage, dirHit, magHit);
            };

            weapon.onHitedSomething = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) => {
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
        if (playerInput.isThrowPressed()) weapon.Attack();
        if (playerInput.isJumpPressed()) base.Jump();

        base.xdir = playerInput.GetMoveXDir();

        weapon.SetAim(playerInput.GetAimDir());

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

    public override void Hurt(float damage, Vector2 dirHit, float magHit)
    {
        base.Hurt(damage, dirHit, magHit);
    }
}
