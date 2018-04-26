using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour {

	// Use this for initialization
	//For Enemy Shooting
	[SerializeField]
	GameObject bullet;

	float fireRate;
	float nextFire;

	//ForBossSFX
	public AudioSource EnemyAttack;
	public AudioSource EnemyDeath;
	public AudioSource DefeatBoss;

	//ForBossAnim
	public Animator animationController;

	//for EnemyHealth
	public int EnCurrentHealth;
	public int EnMaxHealth = 100;

	//for mainMenu
	public GameObject YouWinText, MainMenuButton;

	//For Enemy Detection
	private Rigidbody2D enemyRB;

	//for enemy patrol
	public Transform originPoint;
	public Transform originPoint2;
	//private Vector2 dir = new Vector2 (-1, 0);
	public float range;
	public float range2;
	public float speed;

	//particle
	public ParticleSystem redfireSystem;

	//For Enemy Detection
	public float radius;

	// Added by Nick
	public float moveVelocity;
	private int direction = 1;
	private bool patrolling = true;

	private float pathLOS = 1f;
	private float LOS_behind = -2.5f;

	Ray pathRay;

	// Use this for initialization
	void Start () {

		fireRate = 2f;
		nextFire = Time.time;

		//for Enemy health
		EnCurrentHealth = EnMaxHealth;

		//You win text
		YouWinText.SetActive(false);
		MainMenuButton.SetActive (false);

		//Enemy Detection
		enemyRB = GetComponent<Rigidbody2D> ();
		StartCoroutine ("Patrol");

	}

	// Update is called once per frame
	void Update () {
		CheckIfTimeToFire ();

		//for enemy health
		if (EnCurrentHealth > EnMaxHealth) {
			EnCurrentHealth = EnMaxHealth;
		}
		//if player loses all health, Restart
		if (EnCurrentHealth <= 0) {
			Destroy (gameObject);
			Die();
		}

		Collider2D Detected = Physics2D.OverlapCircle (transform.position, radius, LayerMask.NameToLayer ("Player"));
		//for player detection
		if (Detected != null)
		{

			CheckIfTimeToFire ();
			Debug.Log ("Boss has detected player!" + Detected.name);
		}
			

		RaycastHit2D path = Physics2D.Raycast (transform.position, transform.localScale.x * Vector2.left * pathLOS, pathLOS);

	if (path.collider != null && path.collider.tag == "Edge")
	{
		Debug.Log ("BOSS HIT EDGE");
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
	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Bullet") {
			//Destroy (col.gameObject);
			//Destroy (gameObject);
			EnMaxHealth -= 25;
			Debug.Log ("Player has hit Enemy!");
			EnemyDeath.Play ();
			redfireSystem.Play ();
		}
	}


	//Enemy bullet fire time
	void CheckIfTimeToFire()
	{
		if (Time.time > nextFire) 
			{
			Instantiate (bullet, transform.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
			EnemyAttack.Play ();
			animationController.Play ("BossAttack");
		} else {animationController.Play ("BossWalking");
			}

	}

	//so enemy flips
	void Flip(){
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

	void Die (){
		YouWinText.SetActive (true);
		MainMenuButton.SetActive (true);
		gameObject.SetActive (false);
		DefeatBoss.Play ();
	}
}
