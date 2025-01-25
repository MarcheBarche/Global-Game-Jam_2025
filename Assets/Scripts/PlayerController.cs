using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public float jumpForce = 5f; // Force applied for jumping
    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded = false;
    public Transform braccio; // The GameObject to rotate
    private InputSystem_Actions inputActions;
    public static Vector3 spawnPoint = Vector3.zero;

    private bool isBubbled = false;
    private int escapeBubbleIndex = 10;
    private int currentEscapeBubbleIndex = 0;

    [SerializeField] private GameObject bubble;
    
    [SerializeField] private LayerMask groundLayer; // Layer mask to check for ground
    [SerializeField] private Transform groundCheck; // Transform to check if grounded
    [SerializeField] private GameObject bubbleGameObject;
    [SerializeField] private Transform shootPoint;
    private float groundCheckRadius = 0.2f; // Radius of ground check

    [SerializeField] private float jumpCooldown = .5f;
    private float lastJump = 0f;

    void Awake()
    {
        this.transform.position = spawnPoint;
        rb = GetComponent<Rigidbody2D>();
        lastJump = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bubble" && collision.gameObject.GetComponent<BubbleController>().parentPlayer != this)
        {
            Bubbled();
            //this.transform.parent = collision.transform;
        }
    }

    private void Bubbled()
    {
        isBubbled =true;
        isGrounded = false;
        this.bubble.SetActive(true);
        this.GetComponent<Animation>().Play();
    }

    private void EscapedBubbled()
    {
        isBubbled = false;
        currentEscapeBubbleIndex = 0;
        escapeBubbleIndex *= 2;
        this.bubble.SetActive(false);
        this.GetComponent<Animation>().Stop();


    }


    void FixedUpdate()
    {
        // Apply horizontal movement using Rigidbody2D physics
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnMove(InputAction.CallbackContext ctx) => moveInput = !this.isBubbled ? ctx.ReadValue<Vector2>().x : 0;
    public void OnRotate(InputAction.CallbackContext ctx) => RotateTarget(ctx.ReadValue<Vector2>());
    public void OnJump(InputAction.CallbackContext ctx) => Jump();
    public void OnShoot(InputAction.CallbackContext ctx) => Shoot();

    private void Shoot() {
        var bubble = Instantiate(bubbleGameObject, shootPoint);
        bubble.GetComponent<BubbleController>().parentPlayer = this;
        bubble.transform.parent = null;
        bubble.transform.rotation = braccio.rotation;
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
        if (isGrounded && Time.time - lastJump >= jumpCooldown)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            lastJump = Time.time;
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
