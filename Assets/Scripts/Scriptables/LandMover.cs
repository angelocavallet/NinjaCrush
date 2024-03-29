using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new LandMover", menuName = "ScriptableObjects/Movements/LandMover")]
public class LandMover : ScriptableObject
{
    [Header("Physics Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float recoverColdown;
    [SerializeField] private bool hasDoubleJump;
    [SerializeField] private bool hasWallJump;

    [Header("Collision Tags")]
    [SerializeField] private string TAG_GROUND = "Ground";

    [Header("Animation Labels")]
    [SerializeField] private string ANIM_MOVING = "Moving";
    [SerializeField] private string ANIM_JUMPING = "Jumping";
    [SerializeField] private string ANIM_FALLING = "Falling";
    [SerializeField] private string ANIM_GROUNDED = "Grounded";
    [SerializeField] private string ANIM_HURT = "Hurt";

    [Header("Horizontal Direction")]
    public float xdir;

    public Transform transform { private get; set; }
    public Rigidbody2D rigidbody2D { private get; set; }
    public Animator animator { private get; set; }
    public SpriteRenderer spriteRenderer { private get; set; }
    public Vector2 groundTouchDirCheck { get; private set; }
    public Vector2 otherTouchDirCheck { get; private set; }
    public float health { get; private set; }
    public Boolean recovering { get; private set; }

    private bool dieTrigger;
    private bool dead;
    private bool moving;
    private Vector2 velocity;
    private bool jumpTrigger;
    private bool doubleJumpCharged = false;
    private float recoveringTimer = 0f;

    public LandMover Clone()
    {
        LandMover landMover = Instantiate(this);
        landMover.health = maxHealth;
        landMover.recovering = false;

        return landMover;
    }

    public void UpdateMovement()
    {
        velocity = rigidbody2D.velocity;

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

        rigidbody2D.velocity = velocity;
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

    public void Hurt(float damage, Vector2 dirHit, float magHit)
    {
        health -= Mathf.Abs(damage);

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
            rigidbody2D.velocity = dirHit * magHit;
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
