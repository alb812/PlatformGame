using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public float velX = 5f;
	float velY = 0f;
	public Rigidbody2D rb;

	void Start(){

		rb = GetComponent<Rigidbody2D> ();
		Destroy (gameObject, 3f);

	}


	void Update(){

		rb.velocity = new Vector2 (velX, velY);

	}
	void onCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground") {
			Destroy (gameObject);
		}
	}
}
