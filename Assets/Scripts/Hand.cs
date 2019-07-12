using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Mob
{

    public float cooldownMin;
    public float cooldownMax;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(crush(Random.Range(cooldownMin, cooldownMax)));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator crush(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!dead)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            gameObject.GetComponent<WayPoints>().speed = 0;
            StartCoroutine(relax(1f));

        }
    }

    private IEnumerator relax(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!dead)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            gameObject.GetComponent<WayPoints>().speed = 6;
            StartCoroutine(crush(Random.Range(cooldownMin, cooldownMax)));
        }
    }
}