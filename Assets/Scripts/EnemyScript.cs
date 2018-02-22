﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	//For Enemy Shooting
	[SerializeField]
	GameObject bullet;

	float fireRate;
	float nextFire;

	//For Enemy Detection
	Rigidbody2D enemyRB;

	// Use this for initialization
	void Start () {

		fireRate = 1f;
		nextFire = Time.time;

		//Enemy Detection
		enemyRB = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		CheckIfTimeToFire ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			

		}
	}

	//If player bullet hits enemy, enemy is destroyed
	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Bullet") {
			Destroy (col.gameObject);
			Destroy (gameObject);
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