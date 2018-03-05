using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManabarScript : MonoBehaviour {

	Image manaBar;
	float maxMana = 150f;
	public static float Mana;

	// Use this for initialization
	void Start () {

		manaBar = GetComponent<Image> ();
		Mana = maxMana;
		
	}
	
	// Update is called once per frame
	void Update () {

		manaBar.fillAmount = Mana / maxMana;
	}
}
