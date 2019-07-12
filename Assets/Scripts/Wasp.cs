using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : Mob
{
    public float cooldownMin;
    public float cooldownMax;
    public GameObject projectile;

    private Transform StingTransform;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.Find("Sprite").GetComponent<Animator>();
        StartCoroutine(fire(Random.Range(cooldownMin, cooldownMax)));
    }

    // Update is called once per frame
    void Update()
    {
        if (animator)
        {
            animator.SetBool("fire", false);
        }
        if (!dead)
        {
            Transform player = GetTransformPlayer();
            if (player.position.x < transform.position.x)
            {
                transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            } else
            {
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }
    }

    private IEnumerator fire(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!dead)
        {
            if (animator)
            {
                animator.SetBool("fire", true);
            }
            StingTransform = transform.Find("Sting").transform;
            Transform player = GetTransformPlayer();
            Vector3 difference = player.position - StingTransform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            GameObject sting = Instantiate(projectile, StingTransform.position, Quaternion.Euler(0.0f, 0.0f, rotZ));
            sting.GetComponent<Sting>().damage = damage;
            StartCoroutine(fire(Random.Range(cooldownMin, cooldownMax)));
        }
    }
}
