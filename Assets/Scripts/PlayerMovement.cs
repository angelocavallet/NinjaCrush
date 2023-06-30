using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float jumpForce = 5f;
    public float speed = 8f;
    public InputAction moveInput;
    public InputAction jumpInput;
    private Rigidbody2D rb2D;
    private BoxCollider2D bxCol2D;
    private Animator anim;
    private SpriteRenderer sprr;
    private float xdir;
    private bool moving;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprr = GetComponent<SpriteRenderer>();
        bxCol2D = GetComponent<BoxCollider2D>();

        moveInput.Enable();
        jumpInput.Enable();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        xdir = moveInput.ReadValue<float>();

        rb2D.velocity = new Vector2(xdir * speed, rb2D.velocity.y);

        moving = (xdir != 0);

        if (moving) {
            sprr.flipX = (xdir < 0f);
        }

        if (jumpInput.IsPressed() && grounded) {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y + jumpForce);
            grounded = false;
        }

        if (! grounded) {
            anim.SetBool("Falling", (rb2D.velocity.y < 0));
            anim.SetBool("Jumping", (rb2D.velocity.y > 0));
        }
        
        anim.SetBool("Moving", moving);
        anim.SetBool("Grounded", grounded);

        Debug.Log(grounded);

    }
    
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") {
            foreach(ContactPoint2D contact in col.contacts) {
                if (Vector2.Angle(contact.normal, Vector2.up) <= 30f) {
                    grounded = true;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            grounded = false;
        }
    }
}
