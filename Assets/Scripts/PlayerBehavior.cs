using System.Collections;
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

	//for player animation during movement
	//public PlayerAnimation playerAnim;

	//to flip sprite
	SpriteRenderer spriteRenderer;

	void Start ()
	{
		//For Jump movements 
		rb = GetComponent<Rigidbody2D> ();
		jumpForce = 300;
		isJumping = false;

		//For Player animation
		//playerAnim = GameObject.Find("Player").GetComponent<PlayerAnimation>();
		//playerAnim.playerSpriteRenderer.sprite = playerAnim.idleSprite;
		//playerAnim.playerSpriteRenderer.sprite = playerAnim.movingSprite;
		//playerAnim.playerSpriteRenderer.sprite = playerAnim.jumpingSprite;
		//playerAnim.playerSpriteRenderer.sprite = playerAnim.attackingSprite;


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
			//playerAnim.playerSpriteRenderer.sprite = playerAnim.movingSprite;

			//to flip left
			spriteRenderer.flipX = false;
		}
		//Player movement right
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
			//playerAnim.playerSpriteRenderer.sprite = playerAnim.movingSprite;

			//to flip right
			spriteRenderer.flipX = true;
		}
		//Player movement for jump
		if (Input.GetButtonDown ("Jump") && isTouchingGround) 
		{
			rb.AddForce (new Vector2(0, jumpForce));
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