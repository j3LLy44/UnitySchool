using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //Health variables
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    //Invincibility variables
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    //walkspeed variable
    public float walkSpeed = 1f;
    float horizontalAxis;
    float verticalAxis;

    //Shoot variables
    public float shootForce = 300f;

    //Vectors
    Vector2 currentPosition;
    Vector2 lookDirection = new Vector2(1, 0);

    //Components
    Rigidbody2D rb;
    Animator anim;

    //Gameobjects
    public GameObject projectilePrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontalAxis, verticalAxis);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();

            anim.SetFloat("Look X", lookDirection.x);
            anim.SetFloat("Look Y", lookDirection.y);
            anim.SetFloat("Speed", move.magnitude);
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        currentPosition = rb.position;
        //X movement
        currentPosition.x += walkSpeed * horizontalAxis * Time.deltaTime;
        //Y movement
        currentPosition.y += walkSpeed * verticalAxis * Time.deltaTime;
        rb.MovePosition(currentPosition);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            anim.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("Health: " + currentHealth + "/" + maxHealth);
    }

    //Throws projectile
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, shootForce);

        anim.SetTrigger("Launch");
    }
}
