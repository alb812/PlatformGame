using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

	float moveSpeed = 5f;

	private Rigidbody2D rb;

	PlayerBehavior target;
	Vector2 moveDirection;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		//so bullet follows player
		target = GameObject.FindObjectOfType<PlayerBehavior> ();
		//bullet movement
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		//bullet speed
		rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
		//bullet is destroyed after time
		Destroy (gameObject, 3f);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D col){
		//if enemy bullet hits player, it is destroyed
		if (col.gameObject.name == "Player") {
			Debug.Log ("Enemy Bullet has Hit!");
			Destroy (gameObject);
		}
		//if enemy bullet hits ground, it is destroyed
		if (col.gameObject.tag == "Ground") {
			Debug.Log ("Hit surface!");
			Destroy (gameObject);
		}
	}
}