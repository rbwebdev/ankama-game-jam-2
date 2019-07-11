using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float hearthPoint;
    public float damage;
    public float destroyDelay;

    protected Animator animator;

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
        hearthPoint -= damage;
        if (hearthPoint <= 0)
        {
            Debug.Log("dead");
            Dead();
        }
    }

    protected void Dead()
    {
        dead = true;
        animator.SetBool("isDeath", true);
        Spawner spawner = GameObject.FindGameObjectsWithTag("Spawner")[0].GetComponent<Spawner>();
        spawner.modDead();
        Destroy(gameObject, destroyDelay);
    }
}
