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
	public Transform groundCheck;

	[Header("The gameobject on what the player is grounded")]
	public LayerMask whatIsGround;

	// ghost bullet
	public GhostBullet ghostBullet;

	// box collider
	Vector2 nativeBoxColliderOffset;

	void Awake() {
		playerAnimator = gameObject.GetComponent<Animator>();
		gameObject.GetComponent<Rigidbody2D>().gravityScale = this.gravityScale;
		nativeBoxColliderOffset = GetComponent<BoxCollider2D>().offset;
		ghostBullet.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		Move();
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundRadius, this.whatIsGround);
	}

	void Move() {
		// controls
		moveX = Input.GetAxis("Horizontal");
		playerAnimator.SetBool ("isGrounded", grounded);

		if (Input.GetButtonDown("Jump") && grounded) {
			// the jump function change the collider position
			// to fix the bad sprite position with the animation
			Jump();
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

		if (Input.GetButtonDown("Fire1")) {
			Fire();
		}

		// player directions
		if (moveX < 0.0f && !facingRight) {
			Flip();
		} else if (moveX > 0.0f && facingRight) {
			Flip();
		}


		// physics
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

	void Jump() {
		Vector2 jump = new Vector2(0, jumpForce);
		playerAnimator.SetTrigger("jump");
		GetComponent<Rigidbody2D>().AddForce(jump);
	}

	void Fire() {
		playerAnimator.SetTrigger("attack");
		ghostBullet.gameObject.SetActive(true);
		StartCoroutine(ghostBullet.Hit());
	}

	void Flip() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
