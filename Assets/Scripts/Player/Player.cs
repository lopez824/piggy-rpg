using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Player : MonoBehaviour
{
    private PlayerState currentState;
    private PlayerInputActions playerActions;
    private Quaternion startingRotation;
    private float gravity = -9.81f;
    private bool isGrounded = true;

    public Animator anim;
    public Rigidbody rigidBody;
    public float jumpForce = 0f;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;

    private void Awake()
    {
        playerActions = new PlayerInputActions();
        startingRotation = transform.rotation;
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
    }

    private void Start()
    {
        currentState = new IdleState(anim);
        currentState.enter();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            ChangeState(context);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector3 direction = new Vector3(playerActions.Player.Move.ReadValue<Vector2>().x, 0, playerActions.Player.Move.ReadValue<Vector2>().y);
        Quaternion lookToward = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookToward, Time.deltaTime * rotationSpeed);
        if (context.performed)
            ChangeState(context);
            
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ChangeState(context);
            if (isGrounded == true)
            {
                jumpForce = 5f;
                isGrounded = false;
            }
        }
            
        if (context.canceled)
        {
            ChangeState(context);
            isGrounded = true;
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

    private void OnDisable()
    {
        playerActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(playerActions.Player.Move.ReadValue<Vector2>().x, 0, playerActions.Player.Move.ReadValue<Vector2>().y);
        direction.Normalize();
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
        
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
            transform.rotation = startingRotation;

        if (isGrounded)
        {
            jumpForce = 0;
        }
        else
        {
            jumpForce -= gravity * Time.deltaTime;
        }
        transform.position += transform.up * jumpForce * Time.deltaTime;
        if (direction == Vector3.zero)
            return;
    }
}
