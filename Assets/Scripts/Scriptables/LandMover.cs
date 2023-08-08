using Cinemachine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "NewLandMover", menuName = "Movements/LandMover")]
public class LandMover : ScriptableObject
{
    [Header("Physics Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool hasDoubleJump;

    [Header("Collision Tags")]
    [SerializeField] private String TAG_GROUND = "Ground";

    [Header("Animation Labels")]
    [SerializeField] private String ANIM_MOVING = "Moving";
    [SerializeField] private String ANIM_JUMPING = "Jumping";
    [SerializeField] private String ANIM_FALLING = "Falling";
    [SerializeField] private String ANIM_GROUNDED = "Grounded";

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 velocity;
    private float xdir;
    private bool jumpTrigger;
    private bool doubleJumpCharged;
    private bool moving;
    private Vector2 groundTouchDirCheck;
    private Vector2 otherTouchDirCheck;
    private bool debugEnabled = false;

    public void UpdateMovement()
    {
        velocity = rigidbody2D.velocity;

        if (xdir != 0f)
        {
            float xMoveVel = Math.Abs(velocity.x) + speed;

            if (xMoveVel > maxSpeed)
            {
                xMoveVel = maxSpeed;
            }

            velocity.x = xMoveVel * xdir;
        }

        moving = Math.Abs(velocity.x) > 0.1f;

        if (jumpTrigger)
        {
            jumpTrigger = false;

            if (IsGrounded() || IsOverSomething() || (doubleJumpCharged && hasDoubleJump))
            {
                doubleJumpCharged = IsGrounded() || IsOverSomething();
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
    }

    public void updateCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TAG_GROUND))
        {
            groundTouchDirCheck = updateVector2Collision(col);
        }
        else
        {
            otherTouchDirCheck = updateVector2Collision(col);
        }
    }

    public void updateCollisionExit(Collision2D col)
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

    public void SetXDir(float xdir)
    {
        this.xdir = xdir;
    }

    public void Jump()
    {
        jumpTrigger = true;
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

    public void SetRigidbody2D(Rigidbody2D rb2D)
    {
        rigidbody2D = rb2D;
    }

    public void SetAnimator(Animator anim)
    {
        animator = anim;
    }

    public void SetSpriteRenderer(SpriteRenderer sprr)
    {
        spriteRenderer = sprr;
    }

    private Vector2 updateVector2Collision(Collision2D col)
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

    public Vector2 GetGroundTouchDirCheck()
    {
        return groundTouchDirCheck;
    }

    public Vector2 GetOtherTouchDirCheck()
    {
        return otherTouchDirCheck;
    }
    
    public void EnableDebug()
    {
        debugEnabled = true;
    }

    public void ToggleDebug()
    {
        debugEnabled = !debugEnabled;
    }

    public LandMover Clone()
    {
        return Instantiate(this);
    }
}
