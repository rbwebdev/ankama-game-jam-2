using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Mob
{
    public float speed;
    private bool follow = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (follow && !dead)
        {
            Transform player = GetTransformPlayer();
            Vector2 target = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            follow = false;
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;
            Collider2D collider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
            Dead();
        }
    }
}
