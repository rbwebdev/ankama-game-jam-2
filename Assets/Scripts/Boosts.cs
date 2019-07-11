using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boosts : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyBoost", 3);
    }

    void DestroyBoost()
    {
        Destroy(gameObject);
    }
}
