using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float hearthPoint;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ici");
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
            Destroy(gameObject);
        }
    }
}
