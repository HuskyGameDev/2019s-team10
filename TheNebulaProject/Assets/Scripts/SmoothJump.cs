using System.Collections;
using UnityEngine;

public class SmoothJump : MonoBehaviour {

	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;

	Rigidbody2D rb2d;

	void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
	}

	void Update() {
        if (rb2d.velocity.y < 1) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1)*Time.deltaTime;

		} else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier -1)*Time.deltaTime;

		}

	}
}
