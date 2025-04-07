using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class EnemyLandMover : LandMover
{
    public const string TAG_GROUND = "Ground";
    public const string TAG_BULLET = "Bullet";

    public long experience
    {
        get => expHolder.experience;
    }

    private System.Random rand = new System.Random();
    private ExperienceHolder expHolder;

    public override void Awake()
    {
        base.Awake();

        expHolder = new ExperienceHolder(120); //todo: move to ExperienceScriptableObject

        if (weapon)
        {
            weapon.onThrowed = (Throwable throwable) =>
            {
                //do something
            };

            weapon.onHitedTarget = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) =>
            {
                collider.GetComponent<EnemyLandMover>().Hurt(throwable.damage, dirHit, magHit);
            };

            weapon.onHitedSomething = (Collider2D collider, Throwable throwable, Vector2 dirHit, float magHit) =>
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
        base.UpdateDirection();
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

    public override void Hurt(float damage, Vector2 dirHit, float magHit)
    {
        base.Hurt(damage, dirHit, magHit);
    }

    private void StartWalk()
    {
        int randXDir = rand.Next(-1, 1);

        if (randXDir == 0) randXDir = 1;

        base.xdir = randXDir;
    }
}
