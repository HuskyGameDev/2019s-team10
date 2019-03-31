using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    public GameObject bam;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //make bomb invisible so that it can destroy bubble afeter animatiton
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

            GameObject b = Instantiate(bam) as GameObject;
            b.transform.position = gameObject.transform.position;
            StartCoroutine(BamAnimation(b));

            FindObjectOfType<ScoreScript>().updateWaterScore(-1);
            FindObjectOfType<ScoreScript>().updateDirtScore(-1);
        }
    }

    //destroy bomb and bam bubble after animation finishes
    private IEnumerator BamAnimation(GameObject b)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(b);
        Destroy(gameObject);
    }

}