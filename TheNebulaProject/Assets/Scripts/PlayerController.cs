using UnityEngine;
using UnityEditor.Animations;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //controls speed/scaling of movement
    [Range(1,25)]
    public float jumpVelocity;
    public float speed;

    //player attributes
    private Rigidbody2D rb2d;
    private float rad;

    //jump object and grounded condition
    public GameObject Jet;
    bool grounded = false;

    //platform transform
    private Vector3 platPos = Vector3.zero;
    public GameObject platformGenerator;

    //animator
    private Animator anim;
    public float iFrameLength = 2;

    // Use this for initialization
    void Start()
    {
        //Get and store references to the Rigidbody2D component, Animator component, and radius for reference.
        rb2d = GetComponent<Rigidbody2D>();
        rad = GetComponent<CircleCollider2D>().radius;
        anim = GetComponent<Animator>();
        
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = (rb2d.velocity * Vector2.up) + (new Vector2(moveHorizontal, 0) * speed);

        //makes player jump
        if (Input.GetKeyDown("space") && (gameObject.transform.position.y - .9f*rad) > platPos.y)
        {
            Debug.Log(grounded);
            //can only jump if grounded
            if (grounded)
            {
                GameObject j = Instantiate(Jet) as GameObject;
                j.transform.position = rb2d.transform.position;
                rb2d.velocity += Vector2.up * jumpVelocity;
                StartCoroutine(JumpAnimation(j));
            }
        }
    }

    //Update is called once per frame
    void Update()
    {
        
    }

    //destroy water jet after animation finishes
    private IEnumerator JumpAnimation(GameObject j) {
        yield return new WaitForSeconds (0.5f);
        Destroy (j);
     }

    //grounded becomes true if the player collides with a platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject p = collision.gameObject;
        platPos = p.transform.position;
        if ((p.CompareTag("Platform") || p.CompareTag("Ground")) && platPos.y < gameObject.transform.position.y) {
            grounded = true;
        }
        if (p.CompareTag("Bomb")) {
            StartCoroutine(damagedBlinker(iFrameLength));
        }
    }

    //ignore collisions with bombs for the given time, enable blinking animation for duration of "immunity"
    IEnumerator damagedBlinker(float time) {
        //ignore collisions with hazards
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, true);

        //Start blinking animation
        anim.SetLayerWeight(1, 1);

        //wait
        yield return new WaitForSeconds(time);

        //enable collisions and stop animation
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
        anim.SetLayerWeight(1, 0);
    }

    //grounded becomes false when player leaves a platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

}