using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string BoostAudio = "event:/UI/Life/Boost";
    public FMOD.Studio.EventInstance UIBoost;
    public FMOD.Studio.ParameterInstance UIBoostPar;
    public string LessLifeAudio = "event:/UI/Life/Life_Less";
    public FMOD.Studio.EventInstance UILesslife;
    public FMOD.Studio.ParameterInstance UILesslifePar;
    public string VOIBoost = "event:/VOI/Apple/VOI_Apple_Boost";
    public FMOD.Studio.EventInstance VoiBoost;
    public FMOD.Studio.ParameterInstance VoiBoostPar;
    public string DPSBoost = "event:/GFX/Spell/GFX_Boost_Wasp";
    public FMOD.Studio.EventInstance dpsboost;
    public FMOD.Studio.ParameterInstance dpsboostPar;
    public string MEDICBoost = "event:/GFX/Spell/GFX_Boost_Worm";
    public FMOD.Studio.EventInstance medicboost;
    public FMOD.Studio.ParameterInstance medicboostPar;
    public string DAMAGEBoost = "event:/GFX/Spell/GFX_Boost_Spider";
    public FMOD.Studio.EventInstance damageboost;
    public FMOD.Studio.ParameterInstance damageboostPar;
    public string DeathAudio = "event:/VOI/Apple/VOI_Apple_Death";
    public FMOD.Studio.EventInstance DeathApple;
    public FMOD.Studio.ParameterInstance DeathApplePar;


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
            Destroy(collision.gameObject);
            MEDICBOOST();
            UIBOOST();
            if (tmpHeal < healthPointsForReset)
            {
                healthPoints = healthPoints + healValue;
            }
            else
            {
                healthPoints = healthPointsForReset;
            }
            printHP();
        }
        if (collision.gameObject.tag == "BoostDamages")
        {
            float boostPoints = collision.gameObject.GetComponent<BoostDamages>().boostPoints;
            float boostTime = collision.gameObject.GetComponent<BoostDamages>().boostTime;
            Destroy(collision.gameObject);
            DAMAGEBOOST();
            UIBOOST();
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
            DSPBOOST();
            UIBOOST();
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
        UILESSLIFE();
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
        DEATHAPPLE();
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

    void UIBOOST()
    {
        UIBoost = FMODUnity.RuntimeManager.CreateInstance(BoostAudio);
        UIBoost.start();
        VoiBoost = FMODUnity.RuntimeManager.CreateInstance(VOIBoost);
        VoiBoost.start();
    }
    void UILESSLIFE()
    {
        UILesslife = FMODUnity.RuntimeManager.CreateInstance(LessLifeAudio);
        UILesslife.start();
    }
    void DSPBOOST()
    {
        dpsboost = FMODUnity.RuntimeManager.CreateInstance(DPSBoost);
        dpsboost.start();
    }
    void DAMAGEBOOST()
    {
        damageboost = FMODUnity.RuntimeManager.CreateInstance(DAMAGEBoost);
        damageboost.start();
    }
    void MEDICBOOST()
    {
        medicboost = FMODUnity.RuntimeManager.CreateInstance(MEDICBoost);
        medicboost.start();
    }
    void DEATHAPPLE()
    {
        DeathApple = FMODUnity.RuntimeManager.CreateInstance(DeathAudio);
        DeathApple.start();
    }
}