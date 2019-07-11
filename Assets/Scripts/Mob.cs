using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float hearthPoint;
    public float damage;
    public float destroyDelay;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        if (hearthPoint < 0)
        {
            Debug.Log("dead");
        }
    }

    protected void Dead()
    {
        dead = true;
        Spawner spawner = GameObject.FindGameObjectsWithTag("Spawner")[0].GetComponent<Spawner>();
        spawner.modDead();
        Destroy(gameObject, destroyDelay);
    }
}
