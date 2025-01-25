using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public float jumpForce = 10f; // Force applied for jumping
    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    public Transform rotationTarget; // The GameObject to rotate
    private InputSystem_Actions inputActions;
    
    [SerializeField]
    private LayerMask groundLayer; // Layer mask to check for ground
    [SerializeField]
    private Transform groundCheck; // Transform to check if grounded
    private float groundCheckRadius = 0.2f; // Radius of ground check

    [SerializeField] private int playerIndex = 0;
    private PlayerInput playerInput;


    void Awake()
    {
        /*
        // Initialize the Input Actions
        inputActions = new InputSystem_Actions();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>().x;
        inputActions.Player.Move.canceled += ctx => moveInput = 0f;
        inputActions.Player.Jump.performed += ctx => Jump();
        inputActions.Player.Look.performed += ctx => RotateTarget(ctx.ReadValue<Vector2>());

        var controllers = InputSystem.devices;
        // Get the Rigidbody2D component attached to the player*/
        rb = GetComponent<Rigidbody2D>();
    }

    /*void OnEnable()
    {
        // Enable the input actions
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        // Disable the input actions
        inputActions.Player.Disable();
    }*/

    void FixedUpdate()
    {
        // Apply horizontal movement using Rigidbody2D physics
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>().x;
    public void OnRotate(InputAction.CallbackContext ctx) => RotateTarget(ctx.ReadValue<Vector2>());

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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
        if (rotationTarget == null || lookInput == Vector2.zero) return;

        // Calculate the angle based on the joystick input
        float angle = Mathf.Atan2(lookInput.y, lookInput.x) * Mathf.Rad2Deg+90;

        // Apply the rotation to the target object
        rotationTarget.rotation = Quaternion.Euler(0, 0, angle);
    }
}
