using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Mob
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform player = GetTransformPlayer();
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
