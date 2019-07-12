using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;

    public GameObject projectile;
    public Transform shotPoint;
    public GameObject sprite;

    private float timeBtwShots;
    public float startTimeBtwShots;

    void Update()
    {
        if (PauseMenu.gameIsPaused == false)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ);

            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(Fire());
                    StartCoroutine(Back());
                    GameObject projectileInstanciate = Instantiate(projectile, shotPoint.position, transform.rotation);
                    projectileInstanciate.GetComponent<Projectile>().damage = damage;
                    timeBtwShots = startTimeBtwShots;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    IEnumerator Fire()
    {
        sprite.GetComponent<SpriteRenderer>().color = new Color(213, 144, 144);
        yield return new WaitForSeconds(0.025f);
        sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    IEnumerator Back()
    {
        sprite.GetComponent<Transform>().localScale = new Vector2(0.8f, 1);
        yield return new WaitForSeconds(0.1f);
        sprite.GetComponent<Transform>().localScale = new Vector2(1, 1);
    }
}
