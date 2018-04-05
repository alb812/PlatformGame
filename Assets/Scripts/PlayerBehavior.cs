using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	//for basic player movement
	public float speed = 2.0f;
	public float jumpSpeedY;
	public Rigidbody2D rb; 

	//For player animation
	public Animator animationController;

	//For playerSFX
	public AudioSource JumpSFX;
	public AudioSource Hit;
	public AudioSource PlayerAttack;
	public AudioSource PlayerDeath;
	public AudioSource PickUp;

	private bool isJumping;

	//so player only jumps once off of the ground 
	public bool isTouchingGround;
	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	//for double jump
	public float delayBeforeDoubleJump;
	public bool canDoubleJump;

	//for stats
	public int currentHealth;
	public int maxHealth = 100;
	public int currentMagic;
	public int maxMagic = 150;

	//for player shooting
	public GameObject bulletToRight, bulletToLeft, gameOverText, restartButton;
	public bool isShooting;
	Vector2 bulletPos;
	public float fireRate =0.7f;
	float nextFire = 0.0f;
	bool facingRight = true;

	//to flip sprite
	SpriteRenderer spriteRenderer;

	void Start ()
	{
		//For Jump movements 
		rb = GetComponent<Rigidbody2D> ();
		isJumping = false;

		//for game over
		gameOverText.SetActive(false);
		restartButton.SetActive (false);


		//For player flip
		spriteRenderer = GetComponent<SpriteRenderer>();

		//For stats
		currentHealth = maxHealth;
		currentMagic = maxMagic;
		isShooting = true;

	}

	void Update ()
	{
		//So player can only jump if they're touching the ground
		isTouchingGround = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayer);

		//When player presses the A Key
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//player moves left
			rb.MovePosition (transform.position += Vector3.left * speed * Time.deltaTime);
			facingRight = false; 

			//to flip left
			spriteRenderer.flipX = false;
			animationController.Play ("PlayerWalking");
		} else {
			animationController.Play("PlayerIdle");
		}
		//When player presses the D Key
		if (Input.GetKey (KeyCode.RightArrow)) {
			//player moves right
			rb.MovePosition (transform.position += Vector3.right * speed * Time.deltaTime);
			facingRight = true;

			//to flip right
			spriteRenderer.flipX = true;
			animationController.Play ("PlayerWalking");
		} else {animationController.Play ("PlayerIdle");
			}
		/*//If player presses the W Key and is touching the ground
			if (Input.GetKey (KeyCode.W) && isTouchingGround) 
				{
				//Adds force to the player jump
				rb.AddForce (new Vector3(0, jumpForce));

				//playerAnim.playerSpriteRenderer.sprite = playerAnim.jumpingSprite;
				}*/

		//For double jump
		if (Input.GetKeyDown (KeyCode.UpArrow) && isTouchingGround) {
			Jump ();
			JumpSFX.Play();
			animationController.Play ("PlayerJump");
		} else {
			animationController.Play("PlayerIdle");
			}

		if (Input.GetKeyDown(KeyCode.UpArrow) && canDoubleJump) 
		{
			Jump ();
			JumpSFX.Play();
			animationController.Play ("PlayerJump");
		} else {
			animationController.Play("PlayerIdle");
			}

		//For player shooting
		if (Input.GetButtonDown ("Jump") && Time.time > nextFire && isShooting == true) {
			nextFire = Time.time + fireRate;
			ManabarScript.Mana -= 10f;
			currentMagic -= 10;
			fire ();
			animationController.Play ("PlayerAttack");
			PlayerAttack.Play ();
		} else {animationController.Play ("PlayerIdle");
			}
		//for player health
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		//if player loses all health, Restart
		if (currentHealth <= 0) {
			Die();
			PlayerDeath.Play();
		}

		//for player magic
		if (currentMagic > maxMagic) {
			currentMagic = maxMagic;
			isShooting = true;
		}
		//if player loses all magic, can't shoot
		if (currentMagic <= 0) {
			isShooting = false;
		}
	}

	void fire(){
		//so bullets change direction with player
		bulletPos = transform.position;
		if (facingRight) 
		{
			bulletPos += new Vector2(+1f, 0.5f);
			Instantiate (bulletToRight, bulletPos, Quaternion.identity);
		}else { 
			bulletPos += new Vector2(-1f, 0.5f);
			Instantiate (bulletToLeft, bulletPos, Quaternion.identity);
		}

	}

	//check for Jump
	void OnCollisionEnter2D(Collision2D col)
	{
		//so player only jumps once off the ground
		if (col.gameObject.tag == "Ground"){ 
			isJumping = false;
			isTouchingGround = true;
			canDoubleJump = false;
			//jumpForce = 2.0f;
		}
		//so player gets GAME OVER when touched by enemy
		if (col.gameObject.tag == "Enemy") {
			currentHealth -= 25;
			HealthBarScript.health -= 25f;
			Debug.Log ("Enemy has hit player!");
		}

		//so player gets GAME OVER when touched by Boss
		if (col.gameObject.tag == "Boss") {
			currentHealth -= 25;
			HealthBarScript.health -= 25f;
			Debug.Log ("Boss has hit player!");
		}

		Debug.Log (col.collider.name);

	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag("HealthPickUp"))
		{
			other.gameObject.SetActive(false);
			Debug.Log ("Player has picked up Health Kit");
			currentHealth = 100;
			HealthBarScript.health = 100f;
			PickUp.Play ();
		}
		//pick up magic items
		if (other.gameObject.CompareTag ("MagicPickUp")) {
			other.gameObject.SetActive (false);
			Debug.Log ("Player has picked up Mana");
			currentMagic = 150;
			ManabarScript.Mana = 150f;
			isShooting = true;
			PickUp.Play();
		}
			//so player gets GAME OVER when touched by enemy bullets
			if (other.gameObject.tag == "EnemyBullet") {
				currentHealth -= 20;
				HealthBarScript.health -= 20f;
				Debug.Log ("Player has been hit by Enemy Bullet!");
				Hit.Play ();
			}
	}

	//for jump 
	void Jump(){

		//for double jump
		if(canDoubleJump){
			canDoubleJump = false;
			rb.AddForce (new Vector2 (rb.velocity.x, jumpSpeedY));

			} 

		//single jump
		if (isTouchingGround) {
			isJumping = true;
			isTouchingGround = false;
			rb.AddForce (new Vector2 (rb.velocity.x, jumpSpeedY));
			Invoke ("EnableDoubleJump", delayBeforeDoubleJump);
		} 

	}

	void EnableDoubleJump(){
		canDoubleJump = true;
	}
				
	void Die (){
		//player restart upon death
		gameOverText.SetActive (true);
		restartButton.SetActive (true);
		gameObject.SetActive (false);
	}

}


