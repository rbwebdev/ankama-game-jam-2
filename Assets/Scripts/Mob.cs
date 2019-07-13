using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Grounded
{
    public float healthPoints;
    public float damage;
    public float destroyDelay;

    public GameObject boost;
    [Range(0f, 100f)]
    public float boostLootPercent;
    public enum DropWhen {onDeath, onTakeDamage}
    public DropWhen dropWhen;

    protected Animator animator;
    private SpriteRenderer sprite;

    public bool dead = false;

    protected Transform GetTransformPlayer()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

        return player.transform;
    }

    protected void DoDamage()
    {

    }

    public void TakeDamage(float damage)
    {
        if (dropWhen == DropWhen.onTakeDamage)
        {
            DropBoost();
        }
        if (sprite == null)
        {
            sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        }
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Dead();
        } else
        {
            StartCoroutine(outch(sprite));
        }
    }

    private IEnumerator outch(SpriteRenderer sprite)
    {
        sprite.enabled = false;
        yield return new WaitForSeconds(0.1f);
        sprite.enabled = true;
        yield return new WaitForSeconds(0.1f);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.1f);
        sprite.enabled = true;
    }

    protected void Dead()
    {
        dead = true;
        if (animator)
        {
            animator.SetBool("isDead", true);
        }
        Spawner spawner = GameObject.FindGameObjectsWithTag("Spawner")[0].GetComponent<Spawner>();
        spawner.modDead();

        if (dropWhen == DropWhen.onDeath)
        {
            DropBoost();
        }

        Destroy(gameObject, destroyDelay);
    }

    protected void DropBoost()
    {
        if (boost)
        {
            if (Random.Range(0f, 100f) <= boostLootPercent)
            {
                GameObject boostInstantiate = Instantiate(boost, transform.position, Quaternion.Euler(0f, 0f, 0f));
                Rigidbody2D rb = boostInstantiate.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(Random.Range(-100f, 100f), 200f));
            }
        }
    }
}
