using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;

    public GameObject projectile;
    public Transform shotPoint;

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
}
