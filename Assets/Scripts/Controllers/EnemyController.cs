using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    public const string TAG_GROUND = "Ground";
    public const string TAG_BULLET = "Bullet";

    [SerializeField] private LandMover enemyLandMover;
    [SerializeField] private StatsInfo statsInfo;

    private System.Random rand = new System.Random();

    public void Awake()
    {
        enemyLandMover = enemyLandMover.Clone();

        enemyLandMover.transform = transform;
        enemyLandMover.rigidbody2D = GetComponent<Rigidbody2D>();
        enemyLandMover.spriteRenderer = GetComponent<SpriteRenderer>();
        enemyLandMover.animator = GetComponent<Animator>();

        statsInfo = statsInfo.Clone(transform, enemyLandMover.health);
    }

    void Start()
    {
        StartWalk();
    }

    public void FixedUpdate()
    {
        if (enemyLandMover.IsWalled()) enemyLandMover.xdir = enemyLandMover.groundTouchDirCheck.x * -1;

        if (enemyLandMover.IsTouchingSomething())
        {
            enemyLandMover.xdir = enemyLandMover.otherTouchDirCheck.x * -1;
            enemyLandMover.Jump();
        }

        if (enemyLandMover.IsOverSomething()) enemyLandMover.Jump();

        enemyLandMover.UpdateMovement();
        enemyLandMover.UpdateAnimation();
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        enemyLandMover.UpdateCollisionStay2D(col);
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        enemyLandMover.UpdateCollisionExit(col);
    }

    public void Hurt(float damage, Vector2 dirHit, float magHit)
    {
        enemyLandMover.Hurt(damage, dirHit, magHit);
        statsInfo.UpdateHealth(-damage);
    }

    private void StartWalk()
    {
        int randXDir = rand.Next(-1, 1);

        if (randXDir == 0) randXDir = 1;

        enemyLandMover.xdir = randXDir;
    }
}
