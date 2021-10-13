using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Public variables
    public float walkSpeed = 10f;
    public float jumpForce = 10f;
    public float maxSpeed = 10f;
    public LayerMask ground;


    public int lives = 5;
    public int maxLives = 10;

    private int score = 0;

    private bool invincible = false;
    public float invincibleTimer = 4;
    private float timeSinceHit = 0;
    private Vector3 lastGroundPosition;

    //Input variables
    float hDirection;
    float jumpDirection;

    //Components
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    public Text healthDisplay;
    public Text scoreDisplay;
    public GameObject deathPanel;
    public GameObject winPanel;

    //Animation conditions
    bool walking;
    bool up;
    bool down;
    bool hit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Get user input
        hDirection = Input.GetAxisRaw("Horizontal");
        jumpDirection = Input.GetAxisRaw("Jump");


        //
        AnimateMovement(hDirection, rb, anim);

        healthDisplay.text = "Lives: " + lives;
        scoreDisplay.text = "Score: " + score;

        if (hit)
        {
            invincible = true;
            timeSinceHit += Time.deltaTime;
            
            if(timeSinceHit >= invincibleTimer)
            {
                hit = false;
                invincible = false;
                timeSinceHit = 0;
            }
        }

        if(coll.IsTouchingLayers(ground))
        {
            lastGroundPosition = transform.position;
        }

        checkDeath();
    }

    void FixedUpdate()
    {
        Movement();

        limitSpeed_X(maxSpeed, rb);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            down = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "fallenOff")
        {
            transform.position = lastGroundPosition;
            ChangeHealth(-2);
        }

        if (other.tag == "Gem")
        {
            Gem gemScript = other.gameObject.GetComponent<Gem>();
            score += gemScript.ScoreToAdd;
            Destroy(other.gameObject);
        }

        if (other.tag == "GameEnd")
        {
            winPanel.SetActive(true);
        }
    }

    private void Movement()
    {
        //Right
        if (hDirection < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            rb.AddForce(new Vector2(hDirection * Time.deltaTime * walkSpeed, 0));
        }
        //Left
        if (hDirection > 0)
        {
            transform.localScale = new Vector2(1, 1);
            rb.AddForce(new Vector2(hDirection * Time.deltaTime * walkSpeed, 0));
        }
        //Jump check
        if (jumpDirection > 0 && coll.IsTouchingLayers(ground))
        {
            Jump(false);
        }
    }

    public void Jump(bool jumpFromEnemy)
    {//Makes the player jump
        up = true;
        if (jumpFromEnemy)
        {
            rb.AddForce(new Vector2(0, jumpForce * 2), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void limitSpeed_X(float speedLimit, Rigidbody2D rb)
    {//Limits speed in the x axis only
        float xVel = rb.velocity.x;
        Vector3 v3 = rb.velocity;
        
        if (xVel > 0 && xVel > speedLimit)
        {
            v3.x = speedLimit;
            rb.velocity = v3;
        }
        if (xVel < 0 && Mathf.Abs(xVel) > speedLimit)
        {
            v3.x = -speedLimit;
            rb.velocity = v3;
        }
    }

    public void ChangeHealth(int amount)
    {//Change the health of the player

        if(amount < 0 && !invincible)
        {
            lives = Mathf.Clamp(lives + amount, 0, maxLives);
            hit = true;
            invincible = true;
        }
        if(amount >0)
        {
            lives = Mathf.Clamp(lives + amount, 0, maxLives);
        }
    }
    
    private void AnimateMovement(float xInput, Rigidbody2D rb, Animator anim)
    {//Sets the motion animation parameters based on current movement
        //walking
        if (xInput != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

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

        //Set animator parameters
        anim.SetBool("Walking", walking);
        anim.SetBool("Up", up);
        anim.SetBool("Down", down);
        anim.SetBool("Hit", hit);
    }

    public void Throw(Vector2 direction)
    {
        rb.AddForce(direction * jumpForce*2, ForceMode2D.Impulse);
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    private void checkDeath()
    {
        if(lives <= 0)
        {
            transform.localScale = new Vector3(0, 0, 0);
            deathPanel.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
