using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	float moveSpeed = 1f;

	private Rigidbody2D rb;

	PlayerBehavior target;
	Vector2 moveDirection;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindObjectOfType<PlayerBehavior> ();
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
		Destroy (gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col){

		if (col.gameObject.name == "Player") {
			Debug.Log ("Hit!");
			Destroy (gameObject);
		}

		if (col.gameObject.name == "Ground") {
			Debug.Log ("Hit!");
			Destroy (gameObject);
		}
	}
}
