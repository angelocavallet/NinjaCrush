using System;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class LandMover : NetworkBehaviour
{
    [SerializeField] private LandMoverScriptableObject landMoverData;

    public Vector2 groundTouchDirCheck { get; private set; }
    public Vector2 otherTouchDirCheck { get; private set; }
    public Boolean recovering { get; private set; }

    protected float xdir;
    protected StatsInfo statsInfo;
    protected Weapon weapon;

    private float maxHealth;
    private float health;
    private float maxSpeed;
    private float speed;
    private float jumpForce;
    private float recoverColdown;
    private bool hasDoubleJump;
    private bool hasWallJump;

    private string TAG_GROUND;

    private string ANIM_MOVING;
    private string ANIM_JUMPING;
    private string ANIM_FALLING;
    private string ANIM_GROUNDED;
    private string ANIM_HURT;

    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool dieTrigger;
    private bool dead;
    private bool moving;
    private Vector2 velocity;
    private bool jumpTrigger;
    private bool doubleJumpCharged = false;
    private float recoveringTimer = 0f;

    public virtual void Awake()
    {
        LoadComponents();
        LoadSOSettings();

        LoadStatsInfo();
        LoadWeapon();
    }

    public void UpdateMovement()
    {
        velocity = rb2D.linearVelocity;

        if (recoveringTimer == 0f && xdir != 0f)
        {
            float xMoveVel = Math.Abs(velocity.x) + speed;

            if (xMoveVel > maxSpeed)
            {
                xMoveVel = maxSpeed;
            }

            velocity.x = xMoveVel * xdir;
        }

        if (recoveringTimer > 0f && (recoveringTimer + recoverColdown < Time.time))
        {
            recovering = false;
            recoveringTimer = 0f;
        }

        moving = Math.Abs(velocity.x) > 0.1f;

        if (jumpTrigger)
        {
            jumpTrigger = false;

            if (CanJump())
            {
                doubleJumpCharged = IsGrounded() || IsOverSomething() || CanWallJump();
                velocity.y += jumpForce;
            }
        }

        rb2D.linearVelocity = velocity;
    }

    [ServerRpc]
    private void UpdateMovementServerRpc()
    {

    }

    public void UpdateAnimation()
    {
        if (moving) spriteRenderer.flipX = velocity.normalized.x < 0.1f;

        animator.SetBool(ANIM_MOVING, moving);
        animator.SetBool(ANIM_GROUNDED, IsGrounded() || IsOverSomething());

        if (!IsGrounded() && !IsOverSomething() && Math.Abs(velocity.y) > 0.1f)
        {
            animator.SetBool(ANIM_FALLING, velocity.y < 0.1f);
            animator.SetBool(ANIM_JUMPING, velocity.y > 0.1f);
        }

        if (dieTrigger)
        {
            animator.SetTrigger(ANIM_HURT);
            xdir = 0f;
            velocity = Vector2.zero;
            dieTrigger = false;
        }
    }

    public void UpdateCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TAG_GROUND))
        {
            groundTouchDirCheck = UpdateVector2Collision(col);
        }
        else
        {
            otherTouchDirCheck = UpdateVector2Collision(col);
        }
    }

    public void UpdateCollisionExit(Collision2D col)
    {
        if (col.gameObject.CompareTag(TAG_GROUND))
        {
            groundTouchDirCheck = Vector2.zero;
        }
        else
        {
            otherTouchDirCheck = Vector2.zero;
        }
    }

    public void Jump()
    {
        jumpTrigger = true;
    }

    public virtual void Hurt(float damage, Vector2 dirHit, float magHit)
    {
        health -= Mathf.Abs(damage);

        statsInfo.UpdateHealth(health);

        if (health < 0f)
        {
            dieTrigger = true;
            dead = true;
            Destroy(transform.gameObject, 0.6f);
        }

        if (magHit > 0f)
        {
            recovering = true;
            recoveringTimer = Time.time;
            rb2D.linearVelocity = dirHit * magHit;
        }
    }

    public Boolean CanJump()
    {
        return IsGrounded() || IsOverSomething() || CanWallJump() || CanDoubleJump();
    }

    public Boolean CanWallJump()
    {
        return hasWallJump && IsWalled();
    }

    public Boolean CanDoubleJump()
    {
        return hasDoubleJump && doubleJumpCharged;
    }

    public Boolean IsGrounded()
    {
        return groundTouchDirCheck.y == Vector2.down.y;
    }

    public Boolean IsWalled()
    {
        return groundTouchDirCheck.x != Vector2.zero.x;
    }

    public Boolean IsTouchingSomething()
    {
        return otherTouchDirCheck.x != Vector2.zero.x;
    }

    public Boolean IsOverSomething()
    {
        return otherTouchDirCheck.y == Vector2.down.y;
    }

    public Boolean IsDead()
    {
        return dead;
    }

    private void LoadComponents()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void LoadSOSettings()
    {
        maxHealth = landMoverData.maxHealth;
        health = maxHealth;
        speed = landMoverData.speed;
        maxSpeed = landMoverData.maxSpeed;
        jumpForce = landMoverData.jumpForce;
        recoverColdown = landMoverData.recoverColdown;
        hasDoubleJump = landMoverData.hasDoubleJump;
        hasWallJump = landMoverData.hasWallJump;

        TAG_GROUND = landMoverData.TAG_GROUND;

        ANIM_MOVING = landMoverData.ANIM_MOVING;
        ANIM_JUMPING = landMoverData.ANIM_JUMPING;
        ANIM_FALLING = landMoverData.ANIM_FALLING;
        ANIM_GROUNDED = landMoverData.ANIM_GROUNDED;
        ANIM_HURT = landMoverData.ANIM_HURT;
    }

    private void LoadStatsInfo()
    {
        statsInfo = GetComponentInChildren<StatsInfo>();

        if (!statsInfo && landMoverData.statsInfoCanvasPrefab)
        {
            statsInfo = Instantiate(landMoverData.statsInfoCanvasPrefab, transform).GetComponent<StatsInfo>();
        }

        if (statsInfo) statsInfo.maxHealth = maxHealth;
    }

    private void LoadWeapon()
    {
        if (!weapon && landMoverData.weaponPrefab)
        {
            weapon = Instantiate(landMoverData.weaponPrefab, transform).GetComponent<Weapon>();
        }
    }

    private Vector2 UpdateVector2Collision(Collision2D col)
    {
        int contactLength = col.GetContacts(col.contacts);

        Vector2 checkTouchDirVector2 = Vector2.zero;

        for (int i = 0; i < contactLength; i++)
        {
            ContactPoint2D contact = col.GetContact(i);

            if (Vector2.Angle(contact.normal, Vector2.up) <= 30f)
            {
                checkTouchDirVector2.y = Vector2.down.y;
            }

            if (Vector2.Angle(contact.normal, Vector2.left) <= 30f)
            {
                checkTouchDirVector2.x = Vector2.right.x;
            }

            if (Vector2.Angle(contact.normal, Vector2.right) <= 30f)
            {
                checkTouchDirVector2.x = Vector2.left.x;
            }
        }

        return checkTouchDirVector2;
    }
}
