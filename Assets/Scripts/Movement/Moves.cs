using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;

    public float groundDrag;

    [Header("Drag")]
    public float playerHeaght;
    public LayerMask GroundLayer;
    bool grounded;


    [Header("Jump")]
    public float jumpForce;
    public float jumpCoolDown;
    public float jumpMultiplier;
    bool canJump;
    public KeyCode jumpKey = KeyCode.Space;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeaght*0.5f + 0.2f);


        InputFunction();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void InputFunction()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void movePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f * jumpMultiplier, ForceMode.Force);
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedSpeed = flatVel.normalized* movementSpeed;
            rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, limitedSpeed.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }

}
