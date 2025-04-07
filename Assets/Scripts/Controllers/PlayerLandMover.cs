using Unity.Burst.Intrinsics;
using Unity.Netcode;
using UnityEngine;

public class PlayerLandMover : LandMover
{
    [SerializeField] private PlayerInputScriptableObject playerInputData;
    [SerializeField] private Transform aimPosition;

    private ExperienceHolder expHolder;
    private PlayerInput playerInput;

    public override void Awake()
    {
        DontDestroyOnLoad(gameObject);

        base.Awake();

        expHolder = new ExperienceHolder(0); //todo: move to load from PlayerData
        playerInput = new PlayerInput(playerInputData);

        if (weapon)
        {
            weapon.onThrowed = (Throwable throwable) => {
                //do something
            };

            weapon.onHitedTarget = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) => {
                EnemyLandMover enemyLandMover = collider.GetComponent<EnemyLandMover>();
                if (!enemyLandMover) return;

                enemyLandMover.Hurt(throwable.damage, dirHit, magHit);

                if (enemyLandMover.IsDead())
                {
                    expHolder.gainExperience(enemyLandMover.experience);
                    Debug.Log($"Matou mais um! XP: {expHolder.experience}");
                }
            };

            weapon.onHitedSomething = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) => {
                //do something
            };
        }

        if (IsOwner && !GameManager.instance.cameraTarget) GameManager.instance.cameraTarget = transform;
    }

    public void Start()
    {
        if (!IsOwner) return;

        playerInput.EnableInputs();
        if (GameManager.instance) GameManager.instance.playerInput = playerInput;
    }

    public void Update()
    {
        base.UpdateDirection();

        if (!IsOwner) return;
        if (playerInput == null) return;

        base.UpdateAnimation();

        base.xdir = playerInput.GetMoveXDir();
        weapon.SetAim(playerInput.GetAimDir());

        if (playerInput.isThrowPressed()) weapon.Attack();
        if (playerInput.isJumpPressed()) base.Jump();
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
