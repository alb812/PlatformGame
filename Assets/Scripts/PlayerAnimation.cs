using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	public Animator animationController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

			if (Input.GetKey (KeyCode.LeftArrow)) {
			animationController.Play("Playerleft");
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				animationController.Play("Playerright");
			}

			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				animationController.Play("PlayerJump");
			}
		
			if (Input.GetButtonDown ("Jump")) {
				animationController.Play("PlayerAttack");
			}

				else{
			animationController.Play("PlayerIdle");
		}
	}
}
