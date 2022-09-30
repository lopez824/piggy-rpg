using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Player : MonoBehaviour
{
    private PlayerState currentState;
    private PlayerInputActions playerActions;
    private Rigidbody rb;
    private Vector2 playerInput = Vector2.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 desiredVelocity = Vector3.zero;

    public Animator anim;
    public Player player;
    public Rigidbody rigidBody;
    public AudioSource sound;
    public float jumpForce = 0f;
    public float movementSpeed = 5f;
    public float movementAccel = 10f;
    public float rotationSpeed = 480f;
    public bool isGrounded = true;

    private void Awake()
    {
        playerActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
    }

    private void Start()
    {
        currentState = new IdleState(player);
        currentState.enter();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            ChangeState(context);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
            ChangeState(context);
        if (context.canceled)
            ChangeState(context);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isGrounded = false;
            ChangeState(context);
            rb.AddForce(Vector2.up * jumpForce);
        }
            
        if (context.canceled)
        {
            ChangeState(context);
        }
    }

    private void ChangeState(InputAction.CallbackContext context)
    {
        PlayerState newState = currentState.handleInput(context);

        if (newState != null)
        {
            currentState.exit();
            currentState = newState;
            currentState.enter();
        }
    }

    private void ChangeGroundState()
    {
        PlayerState newState;
        if (rb.velocity.x < 1 && rb.velocity.z < 1)
            newState = new IdleState(player);
        else
            newState = new WalkingState(player);

        if (newState != null)
        {
            currentState.exit();
            currentState = newState;
            currentState.enter();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGrounded == false)
        {
            isGrounded = true;
            ChangeGroundState();
        }
    }

    private void OnDisable()
    {
        playerActions.Player.Disable();
    }

    private void Update()
    {
        playerInput.x = playerActions.Player.Move.ReadValue<Vector2>().x;
        playerInput.y = playerActions.Player.Move.ReadValue<Vector2>().y;
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * movementSpeed;
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;
        float speedDelta = movementAccel * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, speedDelta);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, speedDelta);

        rb.velocity = velocity;

        if (desiredVelocity == Vector3.zero)
            return;
        Quaternion targetRotation = Quaternion.LookRotation(desiredVelocity);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(targetRotation);
    }
}
