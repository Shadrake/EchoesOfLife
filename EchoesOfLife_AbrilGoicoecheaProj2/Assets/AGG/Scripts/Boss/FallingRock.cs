using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public float damage;
    void Start()
    {
        Destroy(gameObject, 2f);
    }
}
