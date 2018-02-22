﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	//for basic player movement
	public float speed = 2.0f;
	private float jumpForce;
	private Rigidbody2D rb;
	private bool isJumping;

	//so player only jumps once off of the ground
	private bool isTouchingGround;
	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask groundLayer;

	//for player shooting
	public GameObject bulletToRight, bulletToLeft, gameOverText, restartButton;
	Vector2 bulletPos;
	public float fireRate = 0.5f;
	float nextFire = 0.0f;
	bool facingRight = true;

	//to flip sprite
	SpriteRenderer spriteRenderer;

	void Start ()
	{
		//For Jump movements 
		rb = GetComponent<Rigidbody2D> ();
		jumpForce = 70;
		isJumping = false;

		//for gameOver
		gameOverText.SetActive(false);
		restartButton.SetActive (false);


		//For player flip
		spriteRenderer = GetComponent<SpriteRenderer>();

	}

	void Update ()
	{
		//So player can only jump if they're touching the ground
		isTouchingGround = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayer);

		//When player presses the A Key
		if (Input.GetKey (KeyCode.A)) 
		{
			//player moves left
			transform.position += Vector3.left * speed * Time.deltaTime;
			facingRight = false; 

			//to flip left
			spriteRenderer.flipX = false;
		}
		//When player presses the D Key
		if (Input.GetKey (KeyCode.D)) 
		{
			//player moves right
			transform.position += Vector3.right * speed * Time.deltaTime;
			facingRight = true;

			//to flip right
			spriteRenderer.flipX = true;
		}
		//If player presses the W Key and is touching the ground
			if (Input.GetKey (KeyCode.W) && isTouchingGround) 
				{
				//Adds force to the player jump
				rb.AddForce (new Vector3(0, jumpForce));

				//playerAnim.playerSpriteRenderer.sprite = playerAnim.jumpingSprite;
				}
		//For player shooting
		if (Input.GetButtonDown ("Jump") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			fire ();
		}
	}

	void fire(){

		bulletPos = transform.position;
		if (facingRight) 
		{
			bulletPos += new Vector2(+1f, -0.13f);
			Instantiate (bulletToRight, bulletPos, Quaternion.identity);
		}else { 
			bulletPos += new Vector2(-1f, -0.13f);
			Instantiate (bulletToLeft, bulletPos, Quaternion.identity);
		}

	}

	//check for Jump
	void onCollisionEnter2D(Collision2D col)
	{
		//so player only jumps once off the ground
		if (col.gameObject.tag == "Ground" && isTouchingGround){ 
			isJumping = false;
		}
		//so player gets GAME OVER when touched by enemy
		if (col.gameObject.tag == "Enemy") {
			gameOverText.SetActive (true);
			restartButton.SetActive (true);
			gameObject.SetActive (false);
			Debug.Log ("Enemy has touched player");
		}

		if (col.gameObject.tag == "EnemyBullet") {
			gameOverText.SetActive (true);
			restartButton.SetActive (true);
			gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag("HealthPickUp"))
		{
			other.gameObject.SetActive(false);
		}

		}
	}
