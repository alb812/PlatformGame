using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	//For Enemy Shooting
	[SerializeField]
	GameObject bullet;

	float fireRate;
	float nextFire;

	//for EnemyHealth
	public int EnCurrentHealth;
	public int EnMaxHealth = 150;

	//For Enemy Detection
	public float radius;

	// Use this for initialization
	void Start () {

		fireRate = 4f;
		nextFire = Time.time;

		//for Enemy health
		EnCurrentHealth = EnMaxHealth;

		//Enemy Detection
		//enemyRB = GetComponent<Rigidbody2D> ();

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
	}
		

	//If player bullet hits enemy, enemy is destroyed
	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Bullet") {
			//Destroy (col.gameObject);
			//Destroy (gameObject);
			EnCurrentHealth -= 25;
			Debug.Log ("Player has hit Enemy!");
		}
	}
		

	//Enemy bullet fire time
	void CheckIfTimeToFire(){
		if (Time.time > nextFire) {
			Instantiate (bullet, transform.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
		}

	}
}