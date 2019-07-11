using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D playerRB;
    public bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerRB = GetComponentInParent<Rigidbody2D>();
        isGrounded = GetComponentInParent<PlayerController>().isGrounded;
    }

    void Update()
    {
        isGrounded = gameObject.GetComponentInParent<PlayerController>().isGrounded; // Vérifie l'etat Grounded.

        anim.SetFloat("ySpeed", playerRB.velocity.y);
        anim.SetBool("IsGrounded", isGrounded);

        if (Input.GetKey("left") || Input.GetKey(KeyCode.Q))
        {
            if (isGrounded)
            {
                anim.SetBool("IsWalk", true);
            }

        }

        else if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
            if (isGrounded)
            {
                anim.SetBool("IsWalk", true);
            }
        }

        else
        {
            anim.SetBool("IsWalk", false);
        }

        if (isGrounded)
        {
            anim.SetBool("IsGrounded", true);
        }

        else
        {
            anim.SetBool("IsGrounded", false);
        }
    }
}

 /*       if (Input.GetKey("left")) // Action Bouton Gauche.
        {
            if (Input.GetKey("right") == false) // Bloc les action simultané avec la touche Droite.
            {
                if (grounded && faceWall == false)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    anim.SetBool("Run", true);
                }
                if (grounded == false && faceWall == false && grabbed == false)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
        else if (Input.GetKey("right")) // Action Bouton Droite.
        {
            if (Input.GetKey("left") == false) // Bloc les action simultané avec la touche Gauche.
            {
                if (grounded && faceWall == false)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    anim.SetBool("IsWalk", true);
                }
                if (grounded == false && faceWall == false && grabbed == false)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
        else
        {
            anim.SetBool("IsWalk", false);
        }

        // Vérifie si le joueur touche le sol.
        if (grounded)
        {
            anim.SetBool("IsGrounded", true);

            StartCoroutine(RollDelay());
        }

        else
        {
            anim.SetBool("IsGrounded", false);
        }
    }
}
*/
