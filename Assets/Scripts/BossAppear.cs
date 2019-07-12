using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppear : MonoBehaviour, IEvent
{
    public GameObject hand;
    public Transform HandPosition;
    public GameObject BossHealthBar;
    public Transform BossHealthBarPosition;

    private bool GotoBar = false;

    private SpriteRenderer sprite;

    public void Trigger()
    {
        sprite = GameObject.FindGameObjectsWithTag("LightTop")[0].GetComponent<SpriteRenderer>();
        if (sprite)
        {
            StartCoroutine(Appear());
        }
    }

    private void Update()
    {
        if (GotoBar)
        {
            BossHealthBar.transform.position = Vector2.MoveTowards(BossHealthBar.transform.position, BossHealthBarPosition.position, 0.05f);
        }
    }

    private IEnumerator Appear()
    {
        Animator animator = sprite.GetComponent<Animator>();
        animator.SetBool("lightOn", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("lightOn", false);
        Instantiate(hand, HandPosition.position, Quaternion.Euler(0f, 0f, 0f));
        yield return new WaitForSeconds(1f);
        GotoBar = true;
        yield return new WaitForSeconds(2f);
        GotoBar = false;
    }


}
