using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {
    public float speed;
    private Vector3 startPOS;
    GameObject player;
    // Use this for initialization
    void Start () {
        startPOS = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (isMoving() > 0)
        {
            transform.Translate((new Vector3(-1, 0, 0)) * speed * Time.deltaTime);
        } else if(isMoving() == 0)
        {
            transform.Translate((new Vector3(0, 0, 0)) * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate((new Vector3(1, 0, 0)) * speed * Time.deltaTime);
        }

        if (transform.position.x < -20.88 || transform.position.x > 27.1)
        {
            transform.position = startPOS;
        }
	}
    public int isMoving()
    {
        int moving = 0;
        if (player.GetComponent<Rigidbody2D>().velocity.x > 0.1)
            moving = 1;
        if (player.GetComponent<Rigidbody2D>().velocity.x < -0.1)
            moving = -1;
        return moving;
    }
}
