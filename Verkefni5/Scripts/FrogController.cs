using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    //Bounds
    public GameObject leftBounds;
    public GameObject rightBounds;
    private float leftLimit;
    private float rightLimit;
    public bool movingRight = false;
    public LayerMask ground;

    public Vector3 jumpVector;
    public float jumpForce;
    public float maxSpeed = 10f;

    private bool up;
    private bool down;

    public int scoreToAdd = 50;

    //jump timer
    public float timeBetweenJumps;
    private float timer = 0f;
    private bool isJumping;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        leftLimit = leftBounds.transform.position.x;
        rightLimit = rightBounds.transform.position.x;
    }

    void Update()
    {
        //Timer
        if (!isJumping)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenJumps)
            {
                isJumping = true;
                timer = 0;
            }
        }


        //animation
        if (rb.IsTouchingLayers(ground))
        {
            up = false;
            down = false;
        }

        AnimateMovement(rb, anim);

        anim.SetBool("Jump", up);
        anim.SetBool("Fall", down);

        //Checks to see if enemy should move left or right
        if (transform.position.x < leftLimit)
        {
            movingRight = true;
        }
        if (transform.position.x > rightLimit)
        {
            movingRight = false;
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            Jump(movingRight);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController playerScript = other.gameObject.GetComponent<PlayerController>();
            if (other.transform.position.y - 0.7 > transform.position.y)
            {
                Debug.Log("Enemy hit!");
                playerScript.Jump(true);
                playerScript.AddScore(scoreToAdd);
                Destroy(gameObject);
            }
            else
            {
                playerScript.ChangeHealth(-1);
                playerScript.Throw(other.gameObject.transform.position - transform.position);
            }
        }
    }

    private void Jump(bool right)
    {
        if (right && coll.IsTouchingLayers(ground))
        {
            rb.AddForce(new Vector3(jumpVector.x, jumpVector.y, 0) * jumpForce, ForceMode2D.Impulse);
            transform.localScale = new Vector2(-1, 1);
        }
        if (!right && coll.IsTouchingLayers(ground))
        {
            rb.AddForce(new Vector3(-jumpVector.x, jumpVector.y, 0) * jumpForce, ForceMode2D.Impulse);
            transform.localScale = new Vector2(1, 1);
        }
        isJumping = false;
    }

    private void limitSpeed(float speedLimit, Rigidbody2D rb)
    {
        //Checks how fast the object is going and limits its speed if it is going above a certain threshold called 'maxSpeed'
        if (rb.velocity.magnitude > speedLimit)
        {
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }

    private void AnimateMovement(Rigidbody2D rb, Animator anim)
    {//Sets the motion animation parameters based on current movement

        //up
        if (rb.velocity.y > 0.0001)
        {
            up = true;
        }
        else
        {
            up = false;
        }

        //down
        if (rb.velocity.y < -0.0001)
        {
            down = true;
        }
        else
        {
            down = false;
        }
    }
}
