using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;
    public GameObject weaponSprite;
    public TMP_Text textHP;
    //public TMP_Text textAmmo;
    //public TMP_Text textDamages;
    //public TMP_Text textPoints;
    public GameObject DPSBoostedSprite;
    public GameObject DamagesBoostedSprite;

    public float healthPoints;
    
    private float healthPointsForReset;

    private bool dspBoosted = false;
    private bool damagesBoosted = false;
    //private int points = 0;

    float targetMoveSpeed;
    private Vector2 mouse;


    private void Start()
    {
        isGrounded = true;
        healthPointsForReset = healthPoints;
        printHP();
        //printAmmo();
        //printDamages();
        //printPoints();
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
        
        if (PauseMenu.gameIsPaused == false)
        {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Medic")
        {
            float healValue = collision.gameObject.GetComponent<Medic>().healValue;
            float tmpHeal = healthPoints + healValue;
            if (tmpHeal < healthPointsForReset)
            {
                healthPoints = healthPoints + healValue;
            }
            else
            {
                healthPoints = healthPointsForReset;
            }
            printHP();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BoostDamages")
        {
            float boostPoints = collision.gameObject.GetComponent<BoostDamages>().boostPoints;
            float boostTime = collision.gameObject.GetComponent<BoostDamages>().boostTime;
            Destroy(collision.gameObject);
            if (!damagesBoosted)
            {
                damagesBoosted = true;
                StartCoroutine(BoostDamages(boostPoints, boostTime));
            }
        }
        if (collision.gameObject.tag == "BoostDPS")
        {
            float boostMultiplicator = collision.gameObject.GetComponent<BoostDPS>().boostMultiplicator;
            float boostTime = collision.gameObject.GetComponent<BoostDPS>().boostTime;
            Destroy(collision.gameObject);
            if (!dspBoosted)
            {
                dspBoosted = true;
                StartCoroutine(BoostDPS(boostMultiplicator, boostTime));
            }
        }
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        printHP();
        if (healthPoints <= 0)
        {
            GameOver();
        }
    }

    //public void TakePoints(int takePoints)
    //{
    //    points += takePoints;
    //    printPoints();
    //}

    void GameOver()
   {
        Destroy(gameObject);
   }

   private void printHP()
   {
        textHP.text = healthPoints.ToString() + "%";
   }

   //private void printAmmo()
   //{
   //     float tmpStartTimeBtwShots = GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots;
   //     float tmpAmmo = 1 / tmpStartTimeBtwShots;
   //     textAmmo.text = "Ammo: " + tmpAmmo + "/s";
   //}

   //private void printDamages()
   //{
   //     textDamages.text = "Damages: " + GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().damage + "/hit";
   //}

   //private void printPoints()
   //{
   //     textPoints.text = "Points: " + points.ToString();
   //}

    IEnumerator BoostDamages(float boostPoints, float boostTime)
   {
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().damage = GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().damage + boostPoints;
        //printDamages();
        if (dspBoosted)
        {
            DamagesBoostedSprite.GetComponent<RectTransform>().position = new Vector2(197.5f, DamagesBoostedSprite.GetComponent<RectTransform>().position.y);
        }
        else
        {
            DamagesBoostedSprite.GetComponent<RectTransform>().position = new Vector2(129, DamagesBoostedSprite.GetComponent<RectTransform>().position.y);
        }
        DamagesBoostedSprite.SetActive(true);
        yield return new WaitForSeconds(boostTime);
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().damage = GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().damage - boostPoints;
        //printDamages();
        if (dspBoosted)
        {
            DPSBoostedSprite.GetComponent<RectTransform>().position = new Vector2(129, DPSBoostedSprite.GetComponent<RectTransform>().position.y);
        }
        DamagesBoostedSprite.SetActive(false);
    }

    IEnumerator BoostDPS(float boostMultiplicator, float boostTime)
    {
        float tmpStartTimeBtwShots = GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots;
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots = tmpStartTimeBtwShots / boostMultiplicator;
        //printAmmo();
        if (damagesBoosted)
        {
            DPSBoostedSprite.GetComponent<RectTransform>().position = new Vector2(197.5f, DPSBoostedSprite.GetComponent<RectTransform>().position.y);
        }
        else
        {
            DPSBoostedSprite.GetComponent<RectTransform>().position = new Vector2(129, DPSBoostedSprite.GetComponent<RectTransform>().position.y);
        }
        DPSBoostedSprite.SetActive(true);
        yield return new WaitForSeconds(boostTime);
        GameObject.FindGameObjectsWithTag("Weapon")[0].GetComponent<Weapon>().startTimeBtwShots = tmpStartTimeBtwShots;
        //printAmmo();
        if (damagesBoosted)
        {
            DamagesBoostedSprite.GetComponent<RectTransform>().position = new Vector2(129, DamagesBoostedSprite.GetComponent<RectTransform>().position.y);
        }
        DPSBoostedSprite.SetActive(false);
    }
}