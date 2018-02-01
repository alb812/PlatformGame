using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	public GameObject player;       //to insert the player gameObject

	private Vector3 offset;         //Stores offset distance between the player and camera

	// Use this for initialization
	void Start () 
	{
		//Calculates the distance between the player's position and camera's position as the offset.
		offset = transform.position - player.transform.position;
	}

	// Its called after Update each frame
	void LateUpdate () 
	{
		// Makes position of the camera the same as the player.
		transform.position = player.transform.position + offset;
	}
}
