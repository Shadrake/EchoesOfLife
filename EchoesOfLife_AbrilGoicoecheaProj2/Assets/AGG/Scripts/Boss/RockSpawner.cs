using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject[] rocks;
    public Transform[] spawnPoints;
    public float beat = (60 / 105) * 2;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (timer > beat)
        {
            GameObject cube = Instantiate(rocks[Random.Range(0, 1)], spawnPoints[Random.Range(0, 9)]);
            cube.transform.localPosition = Vector3.zero;
            timer -= beat;
        }

        timer += Time.deltaTime;
    }
}
