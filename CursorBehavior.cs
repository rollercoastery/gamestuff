using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The cursor behavior for the player to move towards. This will help with importing the game to mobile.
*/
public class CursorBehavior : MonoBehaviour {

    public Camera cam;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0)) // Left mouse button
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
        
	}
}
