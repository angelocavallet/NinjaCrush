using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    public const string TAG_GROUND = "Ground";
    public const string TAG_BULLET = "Bullet";

    [SerializeField] private LandMover enemyLandMover;

    private bool jumpTrigger = false;

    public void Awake()
    {
        enemyLandMover = enemyLandMover.Clone();
        enemyLandMover.SetRigidbody2D(GetComponent<Rigidbody2D>());
        enemyLandMover.SetSpriteRenderer(GetComponent<SpriteRenderer>());
        enemyLandMover.SetAnimator(GetComponent<Animator>());
    }

    void Start()
    {
        int randXDir = Random.Range(-1, 1);
        
        if (randXDir == 0) randXDir = 1;

        enemyLandMover.SetXDir(1);

    }

    public void FixedUpdate()
    {

        enemyLandMover.UpdateMovement();

        if (enemyLandMover.IsWalled())
        {
            enemyLandMover.SetXDir(enemyLandMover.GetGroundTouchDirCheck().x * -1);
        }

        if (enemyLandMover.IsTouchingSomething())
        {
            enemyLandMover.SetXDir(enemyLandMover.GetOtherTouchDirCheck().x * -1);
        }

        if (enemyLandMover.IsOverSomething())
        {
            enemyLandMover.Jump();
        }
    }

    public void Update()
    {
        if (jumpTrigger) enemyLandMover.Jump();
        enemyLandMover.UpdateAnimation();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.otherCollider.transform.CompareTag(TAG_BULLET))
        {
            enemyLandMover.Hurt();
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        enemyLandMover.updateCollisionStay2D(col);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        enemyLandMover.updateCollisionExit(col);
    }
}
