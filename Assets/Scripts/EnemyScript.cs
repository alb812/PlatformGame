using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	//For Enemy Shooting
	[SerializeField]
	GameObject bullet;

	float fireRate;
	float nextFire;

	//For animation
	public Animator animationController;

	//For EnemySFX
	public AudioSource EnemyAttack;
	public AudioSource EnemyDeath;

	//for EnemyHealth
	public int EnCurrentHealth;
	public int EnMaxHealth = 150;

	//For Enemy Detection
	public float radius;

	//for enemy patrol
	Rigidbody2D enemyRB;
	public Transform originPoint;
	public Transform originPoint2;
	private Vector2 dir = new Vector2 (-1, 0);
	public float range;
	public float range2;
	public float speed;

	// Use this for initialization

	void Start () {

		fireRate = 4f;
		nextFire = Time.time;

		//for Enemy health
		EnCurrentHealth = EnMaxHealth;

		//for enemy patrolling
		enemyRB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		//for enemy health
		if (EnCurrentHealth > EnMaxHealth) {
			EnCurrentHealth = EnMaxHealth;
		}
		//if player loses all health, Restart
		if (EnCurrentHealth <= 0) {
			Destroy (gameObject);
		}

		Collider2D Detected = Physics2D.OverlapCircle (transform.position, radius, LayerMask.NameToLayer("Player"));
		//for player detection
		if (Detected != null) {

			CheckIfTimeToFire ();
			Debug.Log ("Enemy has detected player!" + Detected.name);
		}

		//for enemy patrolling
		Debug.DrawRay(originPoint.position, dir * range);
		RaycastHit2D hit= Physics2D.Raycast(originPoint.position, dir, range);
		RaycastHit2D hit2= Physics2D.Raycast(originPoint2.position, dir, range2);

		if (hit2 == true) {
			if (hit2.collider.CompareTag ("Ground")) {
				Flip ();
				speed *= -1;
				dir *= -1;
			}
		}
		if (hit == false || hit.collider.CompareTag("Player")) {
				Flip();
				speed *= -1;
				dir *= -1;
		}
	}

	void FixedUpdate(){

		enemyRB.velocity = new Vector2 (speed, enemyRB.velocity.y);
	}
		
	//If player bullet hits enemy, enemy is destroyed
	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Bullet") {
			//Destroy (col.gameObject);
			//Destroy (gameObject);
			EnCurrentHealth -= 25;
			Debug.Log ("Player has hit Enemy!");
			EnemyDeath.Play ();
		}
	}
		

	//Enemy bullet fire time
	void CheckIfTimeToFire(){
		if (Time.time > nextFire) {
			Instantiate (bullet, transform.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
			animationController.Play("BadGuyAttack");
			EnemyAttack.Play();
		}
		else{animationController.Play("BadGuyAnim");
		}
	}

	//so enemy flips
	void Flip(){
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
		
}