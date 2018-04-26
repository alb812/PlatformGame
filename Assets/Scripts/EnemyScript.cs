using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

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
	//private Vector2 dir = new Vector2 (-1, 0);
	public float range;
	public float range2;
	public float speed;

	//particle
	public ParticleSystem redfireSystem;

	// Added by Nick
	public float moveVelocity;
	private int direction = 1;
	private bool patrolling = true;

	private float pathLOS = 1f;
	private float LOS_behind = -2.5f;

	Ray pathRay;


	// Use this for initialization

	void Start ()
	{

		fireRate = 4f;
		nextFire = Time.time;

		//for Enemy health
		EnCurrentHealth = EnMaxHealth;

		//for enemy patrolling
		enemyRB = GetComponent<Rigidbody2D> ();

		StartCoroutine ("Patrol");

	}
	
	// Update is called once per frame
	void Update ()
	{


		
		//for enemy health
		if (EnCurrentHealth > EnMaxHealth)
		{
			EnCurrentHealth = EnMaxHealth;
		}
		//if player loses all health, Restart
		if (EnCurrentHealth <= 0)
		{
			Destroy (gameObject);
		}

		Collider2D Detected = Physics2D.OverlapCircle (transform.position, radius, LayerMask.NameToLayer ("Player"));
		//for player detection
		if (Detected != null)
		{

			CheckIfTimeToFire ();
			Debug.Log ("Enemy has detected player!" + Detected.name);
		}

		//for enemy patrolling
		/*Debug.DrawRay(originPoint.position, dir * range);
		RaycastHit2D hit= Physics2D.Raycast(originPoint.position, dir, range);
		RaycastHit2D hit2= Physics2D.Raycast(originPoint2.position, dir, range2);

		//For origin point 2
		if (hit2 == true) {
			if (hit2.collider.CompareTag ("Ground")) {
				Flip ();
				speed *= -1;
				dir *= -1;
			}
		}

		//For origin point 1
		if (hit == false || hit.collider.CompareTag("Player")) {
				Flip();
				speed *= -1;
				dir *= -1;
		}*/
		//RaycastHit2D path = Physics2D.Raycast (transform.position, transform.localScale.x * new Vector2 (-1, 1 * direction) * pathLOS);
		//Debug.DrawRay (transform.position, transform.localScale.x * new Vector2 (-1, 1 * direction) * pathLOS);

		RaycastHit2D path = Physics2D.Raycast (transform.position, transform.localScale.x * Vector2.left * pathLOS, pathLOS);

		//Debug.DrawRay (transform.position, transform.localScale.x * Vector2.left * pathLOS);

		//Flip ();
		//Debug.Log(" ~~~~~~~~~~~~~PATH COLLIDER " +path.collider.name);

		if (path.collider != null && path.collider.tag == "Edge")
		{
			Debug.Log ("HIT EDGE");
			if (patrolling)
			{
				Flip ();
			}
		}

		RaycastHit2D hitBehind = Physics2D.Raycast (transform.position, transform.localScale.x * Vector2.left, LOS_behind);
		if (hitBehind.collider != null && hitBehind.collider.CompareTag ("Player"))
		{
			Flip ();
		}
	}

		
	//If player bullet hits enemy, enemy is destroyed
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Bullet")
		{
			//Destroy (col.gameObject);
			//Destroy (gameObject);
			EnCurrentHealth -= 25;
			Debug.Log ("Player has hit Enemy!");
			EnemyDeath.Play ();
			redfireSystem.Play ();
		}
	}
		

	//Enemy bullet fire time
	void CheckIfTimeToFire ()
	{
		if (Time.time > nextFire)
		{
			Instantiate (bullet, transform.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
			EnemyAttack.Play ();
			animationController.Play ("BadGuyAttack");}
		else
		{
			animationController.Play ("BadGuyAnim");
		}
	}

	//so enemy flips
	void Flip ()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

		// For the patrol LOS detection
		direction *= -1;
	}

	IEnumerator Patrol ()
	{
		while (patrolling)
		{
			// Patrol movement
			moveVelocity = speed * direction;
			enemyRB.velocity = new Vector2 (moveVelocity, enemyRB.velocity.y);
			animationController.Play ("BadGuyAnim");


			// Dont get rid of this...ever
			yield return null;
		}
		
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		//Gizmos.DrawLine (transform.position, transform.position + transform.localScale.x * Vector3.right * LOS);
		//Gizmos.DrawLine (transform.position, transform.position + transform.localScale.x * Vector3.left * LOS_behind);


		Gizmos.color = Color.blue;
		//Gizmos.DrawRay (path);
	}
		
}