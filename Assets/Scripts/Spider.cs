using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Mob
{
    public float speed;
    private bool follow = true;

    void Start()
    {
        //go autodestroy
        //get direction
    }

    void Update()
    {
        Vector2 target = new Vector2(0, 0);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().TakeDamage(damage);
        }
    }
}
