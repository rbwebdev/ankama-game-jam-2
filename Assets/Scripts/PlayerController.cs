using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;
    public GameObject weaponSprite;

    public float healthPoints;

    //private bool isLeft;
    //private bool isRight;

    private float healthPointsForReset;

    float targetMoveSpeed;

    private void Start()
    {
        isGrounded = true;
        healthPointsForReset = healthPoints;
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

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (difference.x < 0)
        {
            weaponSprite.GetComponent<SpriteRenderer>().flipY = true;
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            weaponSprite.GetComponent<SpriteRenderer>().flipY = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Medic")
        {
            float boostPoints = collision.gameObject.GetComponent<Medic>().healValue;
            float healthPointsForReset = healthPoints;
            if (healthPoints + boostPoints > healthPoints)
            {
                healthPoints = healthPointsForReset;
            }
            else
            {
                healthPoints += boostPoints;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BoostDamages")
        {
            float boostPoints = collision.gameObject.GetComponent<BoostDamages>().boostPoints;
            float boostTime = collision.gameObject.GetComponent<BoostDamages>().boostTime;
            StartCoroutine(BoostDamages(boostPoints, boostTime));
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BoostDPS")
        {
            float boostMultiplicator = collision.gameObject.GetComponent<BoostDPS>().boostMultiplicator;
            float boostTime = collision.gameObject.GetComponent<BoostDPS>().boostTime;
            Destroy(collision.gameObject);
            StartCoroutine(BoostDPS(boostMultiplicator, boostTime));
        }
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            GameOver();
        }
    }

   void GameOver()
   {
        Destroy(gameObject);
   }

   IEnumerator BoostDamages(float boostPoints, float boostTime)
   {
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().damage += boostPoints;
        yield return new WaitForSeconds(boostTime);
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().damage -= boostPoints;
    }

    IEnumerator BoostDPS(float boostMultiplicator, float boostTime)
    {
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots /= boostMultiplicator;
        yield return new WaitForSeconds(boostTime);
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots *= boostMultiplicator;
    }
}