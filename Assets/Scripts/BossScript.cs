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

	//for EnemyHealth
	public int EnCurrentHealth;
	public int EnMaxHealth = 100;

	//for mainMenu
	public GameObject YouWinText, MainMenuButton;

	//For Enemy Detection
	private Rigidbody2D enemyRB;

	// Use this for initialization
	void Start () {

		fireRate = 2f;
		nextFire = Time.time;

		//for Enemy health
		EnCurrentHealth = EnMaxHealth;

		YouWinText.SetActive(false);
		MainMenuButton.SetActive (false);


		//Enemy Detection
		enemyRB = GetComponent<Rigidbody2D> ();

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
	}


	//If player bullet hits enemy, enemy is destroyed
	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Bullet") {
			//Destroy (col.gameObject);
			//Destroy (gameObject);
			EnMaxHealth -= 25;
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

	void Die (){
		YouWinText.SetActive (true);
		MainMenuButton.SetActive (true);
		gameObject.SetActive (false);
	}
}
