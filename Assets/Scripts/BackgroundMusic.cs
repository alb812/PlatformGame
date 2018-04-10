using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {


	public AudioSource BossMusica;
	public AudioSource MainMusica;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) 
		{
			//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
			if (other.gameObject.CompareTag("Player"))
			{
			MainMusica.Stop ();
			BossMusica.Play ();
			}
	}
}
