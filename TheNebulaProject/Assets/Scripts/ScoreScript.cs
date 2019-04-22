using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreScript : MonoBehaviour {
    public GameObject LevelStart;
    public GameObject LevelEnd;
    public GameObject Player;
    public Slider LevelProgression;
    public Slider WaterTracker;
    public Slider DirtTracker;
    public Text water_text;
    public Text dirt_text;
    public int water_max;
    public int dirt_max;
    public int water_score = 0;
    public int dirt_score = 0;
    

	// Use this for initialization
	void Start () {
        WaterTracker.maxValue = water_max;
        DirtTracker.maxValue = dirt_max;
        LevelProgression.maxValue = GameObject.Find("PlatformSpawner").GetComponent<SpawnPlatform>().goalNumPlatforms;
        water_text.GetComponent<Text>().text = water_score + " / " + water_max;
        dirt_text.GetComponent<Text>().text = dirt_score + " / " + dirt_max;
    }
	
	// Update is called once per frame
	void Update () {
        LevelProgression.value = GameObject.Find("PlatformSpawner").GetComponent<SpawnPlatform>().numPlatforms;
    }

    // Update the water score
    public void updateWaterScore(int update)
    {
        water_score += update;

        if (water_score < 0)
        {
            water_score = 0;
        }

        WaterTracker.value = water_score;
        water_text.GetComponent<Text>().text = water_score + " / " + water_max;
    }

    public void updateDirtScore(int update)
    {
        dirt_score += update;

        if (dirt_score < 0)
        {
            dirt_score = 0;
        }

        DirtTracker.value = dirt_score;
        dirt_text.GetComponent<Text>().text = dirt_score + " / " + dirt_max;
    }
}
