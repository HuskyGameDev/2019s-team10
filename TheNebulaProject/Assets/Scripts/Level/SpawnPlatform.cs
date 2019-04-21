using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SpawnPlatform : MonoBehaviour {
    public Transform[] spawnLocations;
    public int prevLocation;
    public int currentLocation;
    public GameObject platform;
    public GameObject[] consumables;
    //public GameObject[] whatToSpawnPrefab;
    //public GameObject[] WhatToSpawnClone;
    private Vector2 screenBounds;
    public int numPlatforms;
    public int goalNumPlatforms;

    public float respawnTime = 1.0f;

    public void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        currentLocation = 0;
        prevLocation = 0;
        StartCoroutine(wave());
    }
    //Spawns platform at determined location with determined consumable
    public void spawnPlatform()
    {
        GameObject plat = Instantiate(platform) as GameObject;
        Transform loc = spawnLocations[determineLocation()];
        
        int c = determineConsumable();
        if (c != 3)
        {
            GameObject cons = Instantiate(consumables[c]) as GameObject;
            //cons.transform.position = new Vector2(screenBounds.x, loc.position.y);
            cons.transform.parent = plat.transform.Find("Platform");
            cons.transform.localPosition = new Vector2(-0.25f, 0.5f);
        }
        plat.transform.position = new Vector2(screenBounds.x, loc.position.y);
        numPlatforms++;
        if(numPlatforms >= goalNumPlatforms)
        {
            if((GameObject.Find("Canvas").GetComponent<ScoreScript>().dirt_score >= GameObject.Find("Canvas").GetComponent<ScoreScript>().dirt_max) && 
                (GameObject.Find("Canvas").GetComponent<ScoreScript>().water_score >= GameObject.Find("Canvas").GetComponent<ScoreScript>().water_max))
            {
                SceneManager.LoadScene("WinScreen");
            } else
            {
                SceneManager.LoadScene("LoseScreen");
            }
        }
        
    }
    //Returns number between 0 and 5 based on previous platform location
    private int determineLocation()
    {
        if (currentLocation == 0)
            currentLocation = Random.Range(0, 4);
        else if (currentLocation > spawnLocations.Length - 2)
            currentLocation = Random.Range(2, spawnLocations.Length);
        else 
            currentLocation = Random.Range(0, prevLocation + 2);

        prevLocation = currentLocation;
        return currentLocation;
    }
    //returns 0:dirt, 1: water, 2:bomb, 3:no spawn
    private int determineConsumable()
    {
        int consumable = Random.Range(0,3); //dirt:0, water:1, bomb:2
        if(Random.Range(0,3) == 1)
        {
            return consumable;
        } else
        {
            return 3;
        }
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
