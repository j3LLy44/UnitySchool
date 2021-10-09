using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Speed and direction
    public float speed = 3f;
    public bool vertical = false;

    //Timer variables
    public float changeTime = 3f;
    float timer;
    int direction = 1;

    //logic
    bool broken = true;

    Rigidbody2D rb;
    Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        timer = changeTime;
    }

    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rb.position;
        if (vertical)
        {
            position.y += Time.deltaTime * speed * direction;

            anim.SetFloat("Move X", 0);
            anim.SetFloat("Move Y", direction);
        }
        else
        {
            position.x += Time.deltaTime * speed * direction;
            anim.SetFloat("Move X", direction);
            anim.SetFloat("Move Y", 0);
        }

        rb.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rb.simulated = false;
        anim.SetTrigger("Fixed");
    }
}
