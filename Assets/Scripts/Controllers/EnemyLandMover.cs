using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class EnemyLandMover : LandMover
{
    [SerializeField] private StatsInfo statsInfo;
    [SerializeField] private Weapon enemyWeapon;

    public const string TAG_GROUND = "Ground";
    public const string TAG_BULLET = "Bullet";

    private System.Random rand = new System.Random();

    public void Awake()
    {
        base.Awake();

        if (enemyWeapon)
        {
            /**        statsInfo = statsInfo.Clone(transform, playerLandMover.health);
             *           */


            enemyWeapon.onThrowed = (Throwable throwable) =>
            {
                //do something
            };

            enemyWeapon.onHitedTarget = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) =>
            {
                collider.GetComponent<EnemyLandMover>().Hurt(throwable.damage, dirHit, magHit);
            };

            enemyWeapon.onHitedSomething = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) =>
            {
                //do something
            };
        }
    }

    void Start()
    {
        StartWalk();
    }

    public void FixedUpdate()
    {
        if (base.IsWalled()) base.xdir = base.groundTouchDirCheck.x * -1;

        if (base.IsTouchingSomething())
        {
            base.xdir = base.otherTouchDirCheck.x * -1;
            base.Jump();
        }

        if (base.IsOverSomething()) base.Jump();

        base.UpdateMovement();
        base.UpdateAnimation();
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

    private void StartWalk()
    {
        int randXDir = rand.Next(-1, 1);

        if (randXDir == 0) randXDir = 1;

        base.xdir = randXDir;
    }
}
