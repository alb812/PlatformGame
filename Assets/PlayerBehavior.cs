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
	/*private bool isTouchingGround;
	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask groundLayer;*/

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		jumpForce = 500;
		isJumping = false;

	}

	void Update ()
	{
		//So player can only jump if they're touching the ground
		//isTouchingGround = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayer);

		//Player movement left
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		//Player movement right
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		//Player movement for jump
		if (Input.GetButtonDown ("Jump")) 
		{
			rb.AddForce (new Vector2(0, jumpForce));
		}
	}

	//check for Jump
	void onCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground" /*&& isTouchingGround*/)
			isJumping = false;
	}
}