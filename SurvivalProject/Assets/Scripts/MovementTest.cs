using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    // VARIABLES
    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    private Vector3 moveDirection;
    private Vector3 velocity;
    public bool isGrounded;
    public float groundCheckDistance;
    public LayerMask groundMask;
    public float gravity;
    public Transform orientation;
    public float jumpHeight;
    private float punchCooldown = 0.8f;
    private float nextPunch;
    private float jumpCooldown = 1.5f;
    private float nextJump;
    private float fallTime = 0;

    // REFERENCES
    private CharacterController controller;
    private Animator mAnimator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mAnimator = GetComponentInChildren<Animator>();
    }

    private void Update() 
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get inputs to move (wsad or arrow keys by default)
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        // Make the player move where character is looking
        moveDirection = orientation.forward * moveZ + orientation.right * moveX;

        if(isGrounded)
        {
            mAnimator.SetBool("Falling", false);
            //Do damage to player according to fallTime
            fallTime = 0;

            // Idle, Walking and running
            if(moveDirection == Vector3.zero)
            {
                Idle();
            }
            else if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            // Jumping
            if(Input.GetKeyDown(KeyCode.Space) && Time.time > nextJump)
            {
                nextJump = Time.time + jumpCooldown;
                Jump();
            }
            // Punch
            if(Input.GetMouseButtonDown(0) && Time.time > nextPunch)
            {
                nextPunch = Time.time + punchCooldown;
                Hit();
            }
            // Roll
            if(Input.GetKeyDown(KeyCode.V))
            {
                mAnimator.SetTrigger("Roll");
            }
        }
        else if(!isGrounded)
        {
            Fall();
        }
        
        moveDirection *= moveSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        mAnimator.SetFloat("Speed", 0, 0.15f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        mAnimator.SetFloat("Speed", 0.4f, 0.15f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        mAnimator.SetFloat("Speed", 1, 0.15f, Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        mAnimator.SetTrigger("Jump");
    }

    private void Hit()
    {
        // Deal damage here
        mAnimator.SetTrigger("Hit");
    }

    private void Fall()
    {
        mAnimator.SetBool("Falling", true);
        fallTime += Time.deltaTime;
    }
}
