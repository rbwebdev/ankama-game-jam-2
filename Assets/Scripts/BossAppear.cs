using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppear : MonoBehaviour, IEvent
{
    public Transform HandPosition;
    public GameObject hand;

    private SpriteRenderer sprite;

    public void Trigger()
    {
        sprite = GameObject.FindGameObjectsWithTag("LightTop")[0].GetComponent<SpriteRenderer>();
        if (sprite)
        {
            StartCoroutine(Appear());
        }
    }

    private IEnumerator Appear()
    {
        Animator animator = sprite.GetComponent<Animator>();
        animator.SetBool("lightOn", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("lightOn", false);
        Instantiate(hand, HandPosition.position, Quaternion.Euler(0f, 0f, 0f));
    }


}
