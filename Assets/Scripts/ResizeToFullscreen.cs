using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToFullscreen : MonoBehaviour {

	// Use this for initialization
	void Start () {



        //Get a reference to background spriterenderer
        SpriteRenderer backgroundSpriteRenderer = GetComponent<SpriteRenderer>(); 

        float worldScreenHeight = Camera.main.orthographicSize * 2; 
        float worldScreenWidth = (worldScreenHeight / Screen.height) * Screen.width;

        float imageRescaleWidth = worldScreenWidth / backgroundSpriteRenderer.sprite.bounds.size.x;
        float imageRescaleHeight = worldScreenHeight / backgroundSpriteRenderer.sprite.bounds.size.y;
        transform.localScale = new Vector3(
            imageRescaleWidth,
            imageRescaleHeight, 1);


        Debug.Log("The worldScreenHeight is:" + worldScreenHeight);
        Debug.Log("The worldScreenWidth is:" + worldScreenWidth);

        Debug.Log("The Screen.height is:" + Screen.height);
        Debug.Log("The Screen.width is:" + Screen.width);

        Debug.Log("So rescaled image to: " + imageRescaleWidth + " times its original size in width");
        Debug.Log("So rescaled image to: " + imageRescaleHeight + " times its original size in height");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
