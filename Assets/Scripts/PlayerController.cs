using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;
    public GameObject weapon;

    public float healthPoints;

    private bool isLeft;
    private bool isRight;

    private int medic = 25;

    float targetMoveSpeed;

    private void Start()
    {
        isGrounded = true;
        isRight = true;
        isLeft = false;
        Debug.Log("APPEL HP: " + healthPoints.ToString());
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
            weapon.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
        }
        else
        {
            weapon.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Medic")
        {
            if (healthPoints + medic > 100)
            {
                healthPoints = 100;
            }
            else
            {
                healthPoints += 25;
            }
            Debug.Log("APPEL TAKE MEDIC");
            Debug.Log("APPEL HP: " + healthPoints.ToString());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BoostDamages")
        {
            Destroy(collision.gameObject);
            StartCoroutine(BoostDamages());
        }
        if (collision.gameObject.tag == "BoostDPS")
        {
            Destroy(collision.gameObject);
            StartCoroutine(BoostDPS());
        }
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
<<<<<<< HEAD
=======
        Debug.Log("APPEL HP: " + healthPoints.ToString());
>>>>>>> master
        if (healthPoints <= 0)
        {
            GameOver();
        }
    }

   void GameOver()
   {
        Destroy(gameObject);
        Debug.Log("APPEL IS DEAD: GAME OVER");
   }

   IEnumerator BoostDamages()
   {
        Debug.Log("APPEL TAKE BOOST DAMAGE");
        Debug.Log("DAMAGE BOOSTED: +5");
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots += 5;
        yield return new WaitForSeconds(30);
        Debug.Log("DAMAGE BOOST END");
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().damage -= 5;
    }

    IEnumerator BoostDPS()
    {
        Debug.Log("APPEL TAKE BOOST DPS");
        Debug.Log("DPS BOOSTED: x2");
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots /= 2;
        Debug.Log("DPS BOOSTED: x2 -> " + GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots);
        yield return new WaitForSeconds(30);
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots *= 2;
        Debug.Log("DPS BOOST END -> " + GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots);
    }
}