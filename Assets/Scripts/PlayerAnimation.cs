using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	public Animator animationController;
	public Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			animationController.Play ("PlayerWalking");

		} else if (Input.GetKey (KeyCode.RightArrow)) {
			animationController.Play ("PlayerWalking");
		
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			animationController.Play ("PlayerJump");

		}else if (Input.GetKey (KeyCode.Space)) {
		animationController.Play ("PlayerAttack");
		}
		else{
			animationController.Play("PlayerIdle");
		}
			
	}
}
