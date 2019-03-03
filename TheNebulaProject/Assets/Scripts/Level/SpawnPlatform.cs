using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour {
    public Transform[] spawnLocations;
    public GameObject platform;
    //public GameObject[] whatToSpawnPrefab;
    //public GameObject[] WhatToSpawnClone;
    private Vector2 screenBounds;

    public float respawnTime = 1.0f;

    public void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(wave());
    }

    public void spawnPlatform()
    {
        GameObject a = Instantiate(platform) as GameObject;
        Transform loc = spawnLocations[Random.Range(0, 3)];
        a.transform.position = new Vector2(screenBounds.x, loc.position.y);
    }
    IEnumerator wave()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnPlatform();
        }
    }
}
