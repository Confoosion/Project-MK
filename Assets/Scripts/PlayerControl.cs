using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player Stats")]
    public float speed;
    public int extraJumps;
    [SerializeField] private float jumpPower;
    [SerializeField] private float baseAttack;

    // [SerializeField] private CharacterSO character;  // This is for when we make characters. It will hold the weapon cooldown and weapon attack power. We will code the attacking stuff when we actually have a weapon
    [SerializeField] private PerkSO perk;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontal;
    private bool isFacingRight;
    private int extraJumpCount;

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
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jumping from the ground
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }
        // Double jump
        else if (Input.GetButtonDown("Jump") && !IsGrounded() && extraJumpCount > 0)
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

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

    }

    private bool IsGrounded()
    {
        bool onGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (onGround)
            ResetJumps();

        return onGround;
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
}
