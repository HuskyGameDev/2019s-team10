using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {
    public int water_max = 1;
    public int dirt_max = 1;
    private int water_score = 0;
    private int dirt_score = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Water score: " + water_score +
                  "\nDirt score: " + dirt_score);
	}

    // Update the water score
    public void updateWaterScore(int update)
    {
        water_score += update;

        if (water_score < 0)
        {
            water_score = 0;
        }

        
    }

    public void updateDirtScore(int update)
    {
        dirt_score += update;

        if (dirt_score < 0)
        {
            dirt_score = 0;
        }
    }
}
