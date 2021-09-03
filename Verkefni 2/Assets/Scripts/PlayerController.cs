using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Forces applied to the player to move him.
    public float movementForce;
    public float inAirEffect = 0.5f;
    public float jumpForce;

    //Inputs from unity
    private float horizontalInput;
    private float verticalInput;
    private float jumpInput;

    //Forces stored in these variable after calculations
    private float xForce;
    private float zForce;
    private float yForce;

    //Vector3 forces to apply to player after calculations
    private Vector3 wasd_MovementForce;
    private Vector3 space_MovementForce;

    //Component references
    public Rigidbody rb;
    public LayerMask groundLayers;
    public SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

 //-----------------------------------------------------------------------------------
    void FixedUpdate()
    {
        //Horizontal input and force calaculations
        horizontalInput = Input.GetAxis("Horizontal");
        xForce = horizontalInput * movementForce * Time.deltaTime;

        //Vertical input and force calaculations
        verticalInput = Input.GetAxis("Vertical");
        zForce = verticalInput * movementForce * Time.deltaTime;

        //Jump input and force calculations
        jumpInput = Input.GetAxis("Jump");
        yForce = jumpInput * jumpForce * Time.deltaTime;

        //Applies WASD forces to a Vector3
        wasd_MovementForce = new Vector3(xForce, 0, zForce);

        //Add forces from Vector3 to player if he is on the ground
        if (isGrounded())
        {
            rb.AddForce(wasd_MovementForce, ForceMode.Force);
        }
        else//if he is in the air, let him move with reduced speed
        {
            rb.AddForce(wasd_MovementForce * inAirEffect, ForceMode.Force);
        }

        //Checks if player is grounded and that the user is pressing SPACE
        if (isGrounded() && jumpInput > 0)
        {
            //Applies force to the player to make him jump
            rb.AddForce(new Vector3(0, yForce, 0), ForceMode.Impulse);
        }
    }

//-----------------------------------------------------------------------------------

    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }
}
