using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossumController : MonoBehaviour
{
    //Bounds
    public GameObject leftBounds;
    public GameObject rightBounds;
    private float leftLimit;
    private float rightLimit;
    public bool movingRight = false;

    public float moveForce;
    public float maxSpeed = 10f;

    public int scoreToAdd = 50;
    public int damage = 1;

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
        //Checks to see if enemy should move left or right
        if(transform.position.x < leftLimit)
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
        if(movingRight)
        {
            MoveRight();
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            MoveLeft();
            transform.localScale = new Vector2(1, 1);
        }

        limitSpeed(maxSpeed, rb);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
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

    void MoveRight()
    {
        //Moves the object to the right
        rb.AddForce(new Vector2(moveForce * Time.deltaTime, 0), ForceMode2D.Force);
    }

    void MoveLeft()
    {
        //moves the object to the left
        rb.AddForce(new Vector2(-moveForce * Time.deltaTime, 0), ForceMode2D.Force);
    }

    private void limitSpeed(float speedLimit, Rigidbody2D rb)
    {
        //Checks how fast the object is going and limits its speed if it is going above a certain threshold called 'maxSpeed'
        if (rb.velocity.magnitude > speedLimit)
        {
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }
}