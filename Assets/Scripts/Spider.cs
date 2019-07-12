using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Mob
{
    public float speed;
    private bool follow = true;

    private bool goLeft = false;
    private int carrot = 200;
    void Start()
    {
        Invoke("DestroySpider", 5);
        if (0 < transform.position.x)
        {
            goLeft = true;
            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    void Update()
    {
        if (goLeft)
        {
            carrot = -200;
        }
        Vector2 target = new Vector2(transform.position.x + carrot, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().TakeDamage(damage);
        }
    }

    void DestroySpider()
    {
        Dead();
        Destroy(gameObject);
    }
}
