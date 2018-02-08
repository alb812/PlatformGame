﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	//for basic player movement
	public float speed = 2.0f;
	private float jumpForce;
	private Rigidbody2D rb;
	private bool isJumping;

	//so player only jumps once
	private bool isTouchingGround;
	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask groundLayer;

	//to flip sprite
	SpriteRenderer spriteRenderer;

	void Start ()
	{
		//For Jump movements 
		rb = GetComponent<Rigidbody2D> ();
		jumpForce = 50;
		isJumping = false;

		//For player flip
		spriteRenderer = GetComponent<SpriteRenderer>();

	}

	void Update ()
	{
		//So player can only jump if they're touching the ground
		isTouchingGround = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayer);

		//Player movement left
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.position += Vector3.left * speed * Time.deltaTime;

			//to flip left
			spriteRenderer.flipX = false;
		}
		//Player movement right
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.position += Vector3.right * speed * Time.deltaTime;

			//to flip right
			spriteRenderer.flipX = true;
		}
		//Player movement for jump
		if (Input.GetKey (KeyCode.W) && isTouchingGround) 
		{
			rb.AddForce (new Vector3(0, jumpForce));

			//playerAnim.playerSpriteRenderer.sprite = playerAnim.jumpingSprite;
		}
	}

	//check for Jump
	void onCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground" /*&& isTouchingGround*/)
			isJumping = false;
	}
}