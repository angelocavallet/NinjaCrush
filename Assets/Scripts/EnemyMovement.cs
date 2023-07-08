using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
    private float jumpForce = 5f;
    private bool jumpTrigger = false;
    private float speed = 3f;
    private float moveTrigger = 0f;
    private Rigidbody2D rb2D;
    private Animator anim;
    private SpriteRenderer sprr;
    private float xdir;
    private bool moving;
    private bool grounded;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprr = GetComponent<SpriteRenderer>();
        moveTrigger = Random.Range(-1, 1);

    }

    void FixedUpdate()
    {

        xdir = moveTrigger;

        rb2D.velocity = new Vector2(xdir * speed, rb2D.velocity.y);

        moving = (xdir != 0);

        if (moving)
        {
            sprr.flipX = (xdir < 0f);
        }

        if (jumpTrigger && grounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y + jumpForce);
            jumpTrigger = false;
            grounded = false;
        }

        if (! grounded)
        {
            anim.SetBool("Falling", (rb2D.velocity.y < 0));
            anim.SetBool("Jumping", (rb2D.velocity.y > 0));
        }
        
        anim.SetBool("Moving", moving);
        anim.SetBool("Grounded", grounded);
    }
    
    void OnCollisionStay2D(Collision2D col)
    {
        foreach(ContactPoint2D contact in col.contacts)
        {
            if (col.gameObject.tag == "Ground")
            {
                if (Vector2.Angle(contact.normal, Vector2.up) <= 30f)
                {
                    grounded = true;
                }
            }

            if (Vector2.Angle(contact.normal, Vector2.left) <= 30f)
            {
                moveTrigger = -1;
            }

            if (Vector2.Angle(contact.normal, Vector2.right) <= 30f)
            {
                moveTrigger = 1;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    public Vector2 GetNormalizedVelocity()
    {
        return rb2D.velocity.normalized;
    }
    public Vector2 GetVelocity()
    {
        return rb2D.velocity;
    }
}
