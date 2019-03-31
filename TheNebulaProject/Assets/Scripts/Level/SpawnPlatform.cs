using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour {
    public Transform[] spawnLocations;
    public int prevLocation;
    public int currentLocation;
    public GameObject platform;
    public GameObject[] platforms;
    //public GameObject[] whatToSpawnPrefab;
    //public GameObject[] WhatToSpawnClone;
    private Vector2 screenBounds;

    public float respawnTime = 1.0f;

    public void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        currentLocation = 0;
        prevLocation = 0;
        StartCoroutine(wave());
    }

    public void spawnPlatform()
    {
        GameObject a = Instantiate(platform) as GameObject;
        Transform loc = spawnLocations[determineLocation()];
        a.transform.position = new Vector2(screenBounds.x, loc.position.y);
    }
    private int determineLocation()
    {
        if (currentLocation == 0)
            currentLocation = Random.Range(0, 3);
        else if (currentLocation == 5)
            currentLocation = Random.Range(1, 6);
        else 
            currentLocation = Random.Range(prevLocation-1, prevLocation + 2);

        prevLocation = currentLocation;
        return currentLocation;
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
