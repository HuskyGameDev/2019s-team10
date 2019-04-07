using UnityEngine;
using UnityEditor.Animations;
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

    //platform transform
    private Vector3 platPos = Vector3.zero;
    private float platSpd;
    public GameObject platformGenerator;
    private float appliedSpeed = 0;

    //animator
    private Animator anim;
    public float iFrameLength = 2;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        platSpd = platformGenerator.GetComponent<SpawnPlatform>().platform.GetComponent<PlatformMover>().moveSpeed;
        anim = GetComponent<Animator>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = (rb2d.velocity * Vector2.up) + (new Vector2(moveHorizontal, 0) * speed) - (new Vector2(appliedSpeed,0));
        
        //makes player jump
        if (Input.GetButtonDown("Jump") && (gameObject.transform.position.y-gameObject.GetComponent<CircleCollider2D>().radius > platPos.y))  
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
            if (p.CompareTag("Platform")) {
                appliedSpeed = platSpd;
            }
            else {
                appliedSpeed = 0;
            }
        }
        if (p.CompareTag("Bomb")) {
            StartCoroutine(damagedBlinker(iFrameLength));
        }
    }

    IEnumerator damagedBlinker(float time) {
        //ignore collisions with hazards
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, true);

        //Start blinking animation
        anim.SetLayerWeight(1, 1);
        Debug.Log("its trying,but failing");
        yield return new WaitForSeconds(time);

        //stop animation and enable collisions
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
        anim.SetLayerWeight(1, 0);
    }

    //grounded becomes false when player leaves a platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            appliedSpeed = 0;
        }
    }

}