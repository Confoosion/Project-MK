using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    [Header("Player Stats")]
    public float speed;
    public float chargeSpeed;
    public int extraJumps;
    [SerializeField] private float jumpPower;

    [SerializeField] private PerkSO perk;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool movementLocked = false;
    private bool isCharging = false;

    private float horizontal;
    private bool isFacingRight;
    private int extraJumpCount;
    private bool onGround = true;

    void Start()
    {
        if (perk != null)
        {
            perk.ApplyPerk();
        }

        ResetJumps();
    }

    void Update()
    {
        // Movement
        if (!movementLocked)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            // Check if player is on ground
            onGround = IsGrounded();

            // Jumping from the ground
            if (Input.GetButtonDown("Jump") && onGround)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            }
            // Double jump
            else if (Input.GetButtonDown("Jump") && !onGround && extraJumpCount > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                extraJumpCount--;
            }

            // Dynamic jump height
            if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }

            FlipPlayer();
        }
    }

    void FixedUpdate()
    {
        if (!movementLocked && !isCharging)
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        else
        {
            horizontal = (isFacingRight) ? 1f : -1f;
            rb.linearVelocity = new Vector2(horizontal * chargeSpeed, rb.linearVelocity.y);
        }
    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (grounded)
            ResetJumps();

        return grounded;
    }

    // Flips player into looking left or right
    private void FlipPlayer()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Used if player equips Double Jump Perk
    private void ResetJumps()
    {
        if (extraJumps > 0)
        {
            extraJumpCount = extraJumps;
        }
    }

    public void ChargeMovement(float time)
    {
        StartCoroutine(DoChargeMovement(time));
    }

    IEnumerator DoChargeMovement(float time)
    {
        movementLocked = true;
        isCharging = true;

        yield return new WaitForSeconds(time);

        movementLocked = false;
        isCharging = false;
    }

    public void playerDeath()
    {
        Debug.Log("Player died");
        movementLocked = true;
        //death animation
        Destroy(this.gameObject);

    }
}
