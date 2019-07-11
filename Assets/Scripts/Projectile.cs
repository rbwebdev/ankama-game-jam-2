using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Destroy Collision");
        DestroyProjectile();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Destroy Trigger");
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
