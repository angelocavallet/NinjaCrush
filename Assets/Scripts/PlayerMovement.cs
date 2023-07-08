using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float speed = 8f;
    [SerializeField] private InputAction moveInput;
    [SerializeField] private InputAction jumpInput;

    private Rigidbody2D rb2D;
    private Animator anim;
    private SpriteRenderer sprr;
    private float xdir;
    private bool moving;
    private bool grounded;
    private bool walledLeft;
    private bool walledRight;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprr = GetComponent<SpriteRenderer>();

        moveInput.Enable();
        jumpInput.Enable();
        
    }

    void FixedUpdate()
    {

        xdir = moveInput.ReadValue<float>();

        if ((walledLeft && xdir < 0) ||
            (walledRight && xdir > 0))
        {

            xdir = 0f;
        }

        rb2D.velocity = new Vector2(xdir * speed, rb2D.velocity.y);

        moving = (xdir != 0);

        if (moving)
        {
            sprr.flipX = (xdir < 0f);
        }

        if (jumpInput.IsPressed() && grounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y + jumpForce);
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
        if (col.gameObject.CompareTag("Ground"))
        {
            bool isGrounded = false;
            bool isWalledLeft = false;
            bool isWalledRight = false;

            foreach (ContactPoint2D contact in col.contacts)
            {
                if(Vector2.Angle(contact.normal, Vector2.up) <= 30f)
                {
                    isGrounded = true;
                }

                if (Vector2.Angle(contact.normal, Vector2.right) <= 30f)
                {
                    isWalledLeft = true;
                }

                if (Vector2.Angle(contact.normal, Vector2.left) <= 30f)
                {
                    isWalledRight = true;
                }
            }

            grounded = isGrounded;

            if (! grounded)
            {
                walledLeft = isWalledLeft;
                walledRight = isWalledRight;

            }
            else
            {
                walledLeft = false;
                walledRight = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
            walledLeft = false;
            walledRight = false;
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
