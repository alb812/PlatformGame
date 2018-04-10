using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public float velX = 2f;
	float velY = 0f;
	public Rigidbody2D rb;

	void Start(){

		rb = GetComponent<Rigidbody2D> ();
		Destroy (gameObject, 3f);

	}


	void Update(){

		rb.velocity = new Vector2 (velX, velY);

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		//bullet disappears when hitting ground
		if (col.gameObject.tag == "Ground") {
			Debug.Log ("Player bullet has hit ground");
			Destroy (gameObject);
		}
			//bullet disappears when hitting enemy
			if (col.gameObject.tag == "Enemy") {
				Debug.Log ("Player bullet has hit target");
				Destroy (gameObject);
			}

		//bullet disappears when hitting boss
		if (col.gameObject.tag == "Boss") {
			Debug.Log ("Player bullet has hit Boss");
			Destroy (gameObject);
		}

	}
}
