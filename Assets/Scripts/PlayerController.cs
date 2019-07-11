using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;

    private bool isLeft;
    private bool isRight;

    float targetMoveSpeed;

    private void Start()
    {
        isGrounded = true;
        isRight = true;
        isLeft = false;
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

        if (!isLeft && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            isRight = false;
            isLeft = true;
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        if (!isRight && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            isRight = true;
            isLeft = false;
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
    }
}