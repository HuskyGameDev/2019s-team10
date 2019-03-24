using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //controls speed/scaling of movement
    [Range(1,25)]
    public float jumpVelocity;
    public float speed;

    //rb2d of the player
    private Rigidbody2D rb2d;

    //jump object and grounded condition
    public GameObject Jet;
    bool grounded = false;

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
            //can only jump if grounded
            if (grounded) {
                GameObject j = Instantiate(Jet) as GameObject;
                j.transform.position = rb2d.transform.position;
                rb2d.velocity += Vector2.up*jumpVelocity;
                StartCoroutine(JumpAnimation(j));
            }
        }

    }

    //destroy water jet after animation finishes
    private IEnumerator JumpAnimation(GameObject j) {
        yield return new WaitForSeconds (0.5f);
        Destroy (j);
     }

    //grounded becomes true if the player collides with a platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) {
            grounded = true;
        }
    }

    //grounded becomes false when player leaves a platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
    }

}