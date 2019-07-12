using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Mob
{

    public float cooldownMin;
    public float cooldownMax;
    public float speed;
    public float jerkyTime;
    public float fallingTime;
    public float gravityScale;

    private Rigidbody2D rb;
    private float maxHealthPoints;
    private GameObject BossBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHealthPoints = healthPoints;
        BossBar = GameObject.Find("Bar");
        animator = transform.Find("Sprite").GetComponent<Animator>();
        StartCoroutine(Crush(Random.Range(cooldownMin, cooldownMax)));
        gameObject.GetComponent<WayPoints>().speed = speed;

        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossBar)
        {
            float ratio = healthPoints / maxHealthPoints;
            if (ratio < 0f)
            {
                ratio = 0f;
            }
            BossBar.transform.localScale = new Vector3(ratio, 1f, 1f);
        }

        
    }

    private IEnumerator Crush(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!dead)
        {
            animator.SetBool("shake", true);
            gameObject.GetComponent<WayPoints>().speed = 0;
            StartCoroutine(Falldown(jerkyTime));
        }
    }

    private IEnumerator Falldown(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!dead)
        {
            animator.SetBool("shake", false);
            animator.SetBool("falling", false);
            rb.gravityScale = gravityScale;
            StartCoroutine(Relax(fallingTime));
        }
    }

    private IEnumerator Relax(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!dead)
        {
            rb.gravityScale = 0;
            animator.SetBool("grounded", false);
            gameObject.GetComponent<WayPoints>().speed = speed;
            StartCoroutine(Crush(Random.Range(cooldownMin, cooldownMax)));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().TakeDamage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("falling", false);
            animator.SetBool("grounded", true);
            GameObject player = GetTransformPlayer().gameObject;
            if (player.GetComponent<PlayerController>().isGrounded)
            {
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
            }
            Camera.main.GetComponent<CameraShake>().shakeDuration = 0.1f;
        }
    }
}