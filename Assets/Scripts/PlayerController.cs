using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int playerIndex;


    public float moveSpeed = 5f; // Speed of the player
    public float jumpForce = 12f; // Force applied for jumping
    private float fallMultiplier = 4f;
    private float coyoteTime = 0.2f; // Adjust as needed
    private float coyoteTimeCounter;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded = false;
    public Transform braccio; // The GameObject to rotate
    private InputSystem_Actions inputActions;
    public Vector3 spawnPoint = Vector3.zero;

    private bool isBubbled = false;
    [SerializeField] private int firstEscapeBubbleIndex = 10;
    [SerializeField] private int modifierEscapeBubbleIndex = 10;
    private int escapeBubbleIndex = 10;
    private int currentEscapeBubbleIndex = 0;

    [SerializeField] private GameObject bubble;
    
    [SerializeField] private LayerMask groundLayer; // Layer mask to check for ground
    [SerializeField] private Transform groundCheck; // Transform to check if grounded
    [SerializeField] private GameObject bubbleGameObject;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private int lifes = 4;

    private float groundCheckRadius = 0.2f; // Radius of ground check

    [SerializeField] private float jumpCooldown = .5f;
    private float lastJump = 0f;

    [SerializeField] private float bubbleCooldown = 2f;
    private float lastBubble = 0f;

    public PlayerAnimation playerAnimation { get; private set; }
    [SerializeField] private RuntimeAnimatorController player2Controller;
    [SerializeField] private Sprite player2Braccio;


    [SerializeField] private Color[] bubbleColors;
    public void SetupPlayer()
    {

        if (playerIndex == 1)
        {
            braccio.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = player2Braccio;
            playerAnimation.ChangeController(player2Controller);
        }
        playerAnimation.ChangeStatusAnimation(PlayerAnimation.AnimationStatus.IDLE);
    }

    void Awake()
    {
        this.transform.position = spawnPoint;
        rb = GetComponent<Rigidbody2D>();
        lastJump = Time.time;
        playerAnimation = GetComponent<PlayerAnimation>();
        SetupPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("SPIKE"))
            LoseLife();

        if (collision.gameObject.tag == "bubble" && collision.gameObject.GetComponent<BubbleController>().parentPlayer != this)
        {
            Destroy(collision.gameObject);
            Bubbled();
            //this.transform.parent = collision.transform;
        }
    }

    private void Spawn()
    {
        this.transform.position = spawnPoint;
        this.escapeBubbleIndex = firstEscapeBubbleIndex;
    }

    private void Death()
    {
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private void LoseLife()
    {
        lifes--;
        if (lifes <= 0)
            Death();
        EscapedBubbled();
        Spawn();
    }

    private void Bubbled()
    {
        isBubbled =true;
        isGrounded = false;
        this.bubble.SetActive(true);
    }

    private void EscapedBubbled()
    {
        isBubbled = false;
        currentEscapeBubbleIndex = 0;
        escapeBubbleIndex += modifierEscapeBubbleIndex;
        this.bubble.SetActive(false);
    }


    void FixedUpdate()
    {
        if (isBubbled)
        {
            rb.linearVelocity = Vector2.up * escapeBubbleIndex/10;
            return;
        }

        // Apply horizontal movement using Rigidbody2D physics
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        // Check if the player is grounded
        if (rb.linearVelocity.x < 0)
            this.transform.localScale = new Vector3(-.5f,.5f,.5f);
        if (rb.linearVelocity.x > 0)
            this.transform.localScale = new Vector3(.5f, .5f, .5f);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        Debug.Log($"rb.linearVelocity.x {rb.linearVelocity.x}");
        if (isGrounded) {
            if(playerAnimation._status != PlayerAnimation.AnimationStatus.WALK && (rb.linearVelocity.x >= 1f || rb.linearVelocity.x <= -1f))
            {
                playerAnimation.ChangeStatusAnimation(PlayerAnimation.AnimationStatus.WALK);
            }else if(playerAnimation._status != PlayerAnimation.AnimationStatus.IDLE && (rb.linearVelocity.x < 1f && rb.linearVelocity.x > -1f))
            {
                playerAnimation.ChangeStatusAnimation(PlayerAnimation.AnimationStatus.IDLE);
            }
        }
        else
        {
            if (playerAnimation._status != PlayerAnimation.AnimationStatus.JUMP)
            {
                playerAnimation.ChangeStatusAnimation(PlayerAnimation.AnimationStatus.JUMP);
            }
        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) => moveInput = !this.isBubbled ? ctx.ReadValue<Vector2>().x : 0;
    public void OnRotate(InputAction.CallbackContext ctx) => RotateTarget(ctx.ReadValue<Vector2>());
    public void OnJump(InputAction.CallbackContext ctx) => Jump();
    public void OnShoot(InputAction.CallbackContext ctx) => Shoot();

    private void Shoot() {
        if (isBubbled || Time.time - lastBubble <= bubbleCooldown)
            return;

        var bubble = Instantiate(bubbleGameObject, shootPoint);
        bubble.GetComponent<BubbleController>().parentPlayer = this;
        bubble.transform.parent = null;
        bubble.transform.rotation = braccio.rotation;
        bubble.transform.GetChild(0).GetComponent<SpriteRenderer>().color = bubbleColors[playerIndex];
        lastBubble = Time.time;
    }
    private void Jump()
    {
        if (isBubbled)
        {
            currentEscapeBubbleIndex++;
            if (currentEscapeBubbleIndex >= escapeBubbleIndex)
            {
                EscapedBubbled();
            }
            return;
        }

        if ((isGrounded || coyoteTimeCounter > 0) && Time.time - lastJump >= jumpCooldown)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Set vertical velocity directly
            lastJump = Time.time;
            coyoteTimeCounter = 0; // Reset coyote time after jump
        }

        // Apply extra gravity if the player releases the jump button mid-air for variable jump height
        if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }


    private void OnDrawGizmos()
    {
        // Draw the ground check radius in the editor for debugging
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    public void RotateTarget(Vector2 lookInput)
    {
        if (isBubbled ||  braccio == null || lookInput == Vector2.zero) return;

        // Calculate the angle based on the joystick input
        float angle = Mathf.Atan2(lookInput.y, lookInput.x) * Mathf.Rad2Deg+90;

        // Apply the rotation to the target object
        braccio.rotation = Quaternion.Euler(0, 0, angle);
    }
}
