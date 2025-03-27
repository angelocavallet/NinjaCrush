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

        if (!GameManager.instance.cameraTarget) GameManager.instance.cameraTarget = transform;
    }

    public void Start()
    {
        playerInput.EnableInputs();
        if (GameManager.instance) GameManager.instance.playerInput = playerInput;
    }

    public void Update()
    {
        if (!IsOwner) return;
        if (playerInput == null) return;

        UpdateServerRpc(playerInput.GetMoveXDir(), playerInput.isJumpPressed(), playerInput.isThrowPressed(), playerInput.GetAimDir());
    }

    [ServerRpc]
    public void UpdateServerRpc(float xdir, bool jump, bool attack, Vector2 aim)
    {
        base.xdir = xdir;
        weapon.SetAim(aim);

        if (attack) weapon.Attack();
        if (jump) base.Jump();

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
