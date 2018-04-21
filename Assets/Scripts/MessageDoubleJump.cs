using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDoubleJump : MonoBehaviour {

	[Space(10)]
	[Header("Gui on off")]
	public bool GuiOn;


	[Space(10)]
	[Header("The text for Trigger")]

	public string Text = "Jump";

	public Rect BoxSize = new Rect( 0, 0, 200, 100);

	[Space(10)]

	public GUISkin customSkin;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D mess)
	{
		if (mess.gameObject.tag == "Player")
		{
			GuiOn = true;
			Debug.Log ("Entered Trigger");
		}
	}

	void OnTriggerExit2D (Collider2D mess) 
	{
		if (mess.gameObject.tag == "Player") {
			GuiOn = false;
			Debug.Log ("Exit Trigger");
		}
	}

	void OnGUI()
	{

		if (customSkin != null)
		{
			GUI.skin = customSkin;
		}

		if (GuiOn == true)
		{
			// Makes a group at the center of screen
			GUI.BeginGroup (new Rect ((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
			// Adjusts rectangles are to the group. 

			GUI.Label(BoxSize, Text);

			// Ends the group
			GUI.EndGroup ();

		}


	}

}
