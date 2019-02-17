using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    
    [Range(1,10)]
    public float jumpVelocity;
    bool grounded = false;
    public float speed;
    private Rigidbody2D rb2d;
    

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.velocity = (rb2d.velocity*Vector2.up)+(new Vector2 (moveHorizontal, 0) * speed);
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))  //makes player jump
        {
            if (grounded) {
                rb2d.velocity += Vector2.up*jumpVelocity;
            }
        }

    }

    //check to see if grounded
    void OnTriggerEnter2D(Collider2D obj) {
        if (obj.gameObject.CompareTag("Platform")) {
            grounded = true;
        }
    }


    void OnTriggerExit2D(Collider2D obj) {
        if (obj.gameObject.CompareTag("Platform")) {
            grounded = false;
        }
    }
}