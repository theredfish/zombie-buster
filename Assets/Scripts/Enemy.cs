using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	// startPatrol = spawn point
	public Transform startPatrol;
	private Vector2 endPatrol;
	private bool facingRight = false;

	Rigidbody2D rgbd2;

	void Awake() {
		transform.position = startPatrol.position;
		endPatrol = new Vector2(transform.position.x + 5, transform.position.y);
		rgbd2 = GetComponent<Rigidbody2D>();
	}

	void Start() {
		rgbd2.AddForce(Vector2.up * 6);
	}

	// Update is called once per frame
	void Update () {
		Vector2 currentPosition = transform.position;

		// the enemy reached the end point
		if ((currentPosition.x >= endPatrol.x) && facingRight) {
			Debug.Log("Enemy reached the end");
			Flip();
		// the player reached the start point
		} else if ((currentPosition.x <= startPatrol.position.x) && !facingRight) {
			Debug.Log("Enemy reached the start");
			Flip();
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	public void Kill() {
		Destroy(this);
	}

	void patrol() {

	}
}
