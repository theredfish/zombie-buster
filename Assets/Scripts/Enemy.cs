using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float speed = 1;
	private bool facingRight = false;
	public Transform start;
	public Vector2 end;
	Rigidbody2D rgbd2;
	Vector3 move;

	void Awake() {
		gameObject.transform.position = start.position;
		rgbd2 = GetComponent<Rigidbody2D>();
		Vector3 startPos = start.position;
		end = new Vector2(startPos.x + 2, startPos.y);
		move = new Vector3(speed, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(move * Time.deltaTime);
		Patrol();
	}

	void Flip() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	void Patrol() {
		if (transform.position.x >= end.x) {
			Flip();
			move = -move;
		}

		if (transform.position.x <= start.position.x) {
			Flip();
			move = -move;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Player>().TakeDamage(1);
		}
	}
}
