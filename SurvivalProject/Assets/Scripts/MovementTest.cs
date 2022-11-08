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

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = orientation.forward * moveZ + orientation.right * moveX;

       
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

        moveDirection *= moveSpeed;

        if(isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else if(!isGrounded)
        {
            //Falling animation after jump animation is ended
        }

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

}
