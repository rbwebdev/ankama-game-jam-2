using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;

    float targetMoveSpeed;

    private void Start()
    {
        isGrounded = true;
    }

    private void Update()
    {
        targetMoveSpeed = Mathf.Lerp(rb.velocity.x, Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Time.deltaTime * 10);
        rb.velocity = new Vector2(targetMoveSpeed, rb.velocity.y);

        if (Input.GetKeyDown (KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }
    }
}