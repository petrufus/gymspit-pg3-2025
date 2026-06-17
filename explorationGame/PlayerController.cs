// PlayerController.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 60f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private float fallGravityMultiplier = 1.8f;     // snappier falls
    [SerializeField] private float lowJumpGravityMultiplier = 2.5f;  // variable jump height

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool jumpPressed;
    private bool jumpHeld;
    private bool isGrounded;
    private float baseGravityScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baseGravityScale = rb.gravityScale;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {

        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")) jumpPressed = true;
        jumpHeld = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        ApplyHorizontalMovement();
        ApplyJump();
        ApplyFallTuning();
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
    }

    private void ApplyHorizontalMovement()
    {
        float targetSpeed = moveInput * moveSpeed;
        float speedDifference = targetSpeed - rb.velocity.x; 
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;
        float movement = speedDifference * accelRate * Time.fixedDeltaTime;

        rb.velocity = new Vector2(rb.velocity.x + movement, rb.velocity.y);
    }

    private void ApplyJump()
    {
        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        jumpPressed = false;
    }

    private void ApplyFallTuning()
    {
        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = baseGravityScale * fallGravityMultiplier;
        }
        else if (rb.velocity.y > 0f && !jumpHeld)
        {
            rb.gravityScale = baseGravityScale * lowJumpGravityMultiplier;
        }
        else
        {
            rb.gravityScale = baseGravityScale;
        }
    }
}
