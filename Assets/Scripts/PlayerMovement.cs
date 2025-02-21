using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;       // Movement speed
    [SerializeField] private float jumpForce = 10f;  // Jump height
    [SerializeField] private float fallMultiplier = 3f; // Faster falling gravity
    [SerializeField] private float upGravityMultiplier = 1.5f; // Increased gravity while going up
    [SerializeField] private float lowJumpMultiplier = 2.5f; // More natural jump curve

    private Rigidbody2D body;
    private Animator anim;
    private bool Grounded;
    private float HorizontalValue;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movement
        HorizontalValue = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(HorizontalValue * speed, body.linearVelocity.y);

        // Flip Character
        if (HorizontalValue > 0.01f)
            transform.localScale = new Vector3(0.4f, 0.4f, 1);
        else if (HorizontalValue < -0.01f)
            transform.localScale = new Vector3(-0.4f, 0.4f, 1);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }

        // Animation States
        anim.SetBool("Run", HorizontalValue != 0);
        anim.SetBool("Grounded", Grounded);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        Grounded = false;
    }

    private void FixedUpdate()
    {
        // **Better Jump Gravity**
        if (body.linearVelocity.y > 0)  // Going Up
        {
            body.linearVelocity += Vector2.up * Physics2D.gravity.y * (upGravityMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (body.linearVelocity.y < 0)  // Falling Down
        {
            body.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        // If the player releases the jump button early, reduce height
        if (body.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            body.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            Grounded = true;
    }

    public bool canAttack()
    {
        return true;
    }
}
