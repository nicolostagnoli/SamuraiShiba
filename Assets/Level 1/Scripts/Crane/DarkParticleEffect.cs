using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DarkParticleEffect : MonoBehaviour
{
    public GameObject particlePrefab;

    public  float Rate = 500;

    private float timeSinceLastSpawn = 0;

    private bool darkModeisTrigger;

    private PolygonCollider2D col;

    private void Start()
    {
        col = GetComponent<PolygonCollider2D>();

    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        float correcTimeBetweenSpawns = 1f / Rate;
        while (timeSinceLastSpawn > correcTimeBetweenSpawns && darkModeisTrigger)
        {
            SpawnDarkAlongOutline();
            timeSinceLastSpawn -= correcTimeBetweenSpawns;
        }
    }

    void SpawnDarkAlongOutline()
    {
        if (col != null)
        {
            int pathIndex = Random.Range(0, col.pathCount);
            Vector2[] points = col.GetPath(pathIndex);
            int pointIndex = Random.Range(0, points.Length);

            Vector2 pointA = points[pointIndex];
            Vector2 pointB = points[(pointIndex + 1) % points.Length];

            Vector2 spawnPoint = Vector2.Lerp(pointA, pointB, Random.Range(0f, 1f));

            SpawnDarkAtPosition(spawnPoint + (Vector2) transform.position);
        }
    }

    void SpawnDarkAtPosition(Vector2 position)
    {
        //SimplePool.Spawn(particlePrefab, position, Quaternion.identity);
        Instantiate(particlePrefab, position, Quaternion.identity);
    }

    public void activateDarkMode()
    {
        darkModeisTrigger = true;
    }

    public bool IsDarkModeTriggered()
    {
        return darkModeisTrigger;
    }

}