using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastingInput : MonoBehaviour {

	SpriteRenderer TempEnemy;

	// Use this for initialization
	void Start () {
		TempEnemy = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {

		RaycastHit2D rayCastHit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (rayCastHit.collider != null) {

			//the ray has hit something with a collider on it
			if (rayCastHit.collider.tag == "Enemy") { //check if name of collider matches what we're looking for
				TempEnemy.color = Color.magenta; //we know its cat sprite, so change color 

			} else {
				TempEnemy.color = Color.white;
			}


		}else{ 
			TempEnemy.color = Color.white;

		}
	}
}
