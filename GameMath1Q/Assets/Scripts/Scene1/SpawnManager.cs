using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Transform[] spawnPoints;

    private float gameTimer = 0f;
    private float maxGameTimer = 60f;
    private float spawntimer = 0f;
    private float maxSpawnTimer = 1f;

    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        isPlaying = true;
    }

    void Update()
    {
        if(isPlaying && gameTimer > maxGameTimer)
        {
            isPlaying = false;
        }

        gameTimer += Time.deltaTime;
        spawntimer += Time.deltaTime;

        if(spawntimer > maxSpawnTimer)
        {
            spawntimer = 0f;
        }
    }
}
