using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int playerSpeed = 10;
	public bool facingRight = false;
	public int jumpForce = 500;
	public float moveX;
	public int gravityScale = 3;
	private Animator playerAnimator;

	// ground checks
	private bool grounded = false;

	private float groundRadius = 0.2f;

	[Header("The gameobject to check if the player is grounded")]
	public Transform groudCheck;

	[Header("The gameobject on what the player is grounded")]
	public LayerMask whatIsGround;

	// box collider
	Vector2 nativeBoxColliderOffset;

	void Awake() {
		playerAnimator = gameObject.GetComponent<Animator>();
		gameObject.GetComponent<Rigidbody2D>().gravityScale = this.gravityScale;
		nativeBoxColliderOffset = GetComponent<BoxCollider2D>().offset;
	}

	// Update is called once per frame
	void Update () {
		Move();
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle(this.groudCheck.position, this.groundRadius, this.whatIsGround);

		// reset the collider offset
		// if (grounded) {
		// 	GetComponent<BoxCollider2D>().offset = nativeBoxColliderOffset;
		// }

		Debug.Log(grounded);
	}

	void Move() {
		// controls
		moveX = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Jump") && grounded) {
			// the jump function change the collider position
			// to fix the bad sprite position with the animation
			Jump();
			playerAnimator.SetTrigger("jump");
		}

		// reset the collider offset when the player is grounded
		if (grounded) {
			gameObject.GetComponent<BoxCollider2D>().offset = nativeBoxColliderOffset;
		} else {
			gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
		}

		// anims
		if (moveX != 0.0f) {
			playerAnimator.SetTrigger("walk");
		}

		if (Input.GetButton("Fire1")) {
			playerAnimator.SetTrigger("attack");
		}
		// player directions
		if (moveX < 0.0f && !facingRight) {
			Flip();
		} else if (moveX > 0.0f && facingRight) {
			Flip();
		}

		playerAnimator.SetBool ("isGrounded", grounded);

		// physics
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

	void Jump() {
		Vector2 jump = new Vector2(0, jumpForce);
		GetComponent<Rigidbody2D>().AddForce(jump);
	}

	void Flip() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
