using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 So this is how this state system works:
 	I would have made a regular state machine using normal classes and not interfaces, but I need some functions out of the MonoBehavior class to function properly
 	So I made "scripts" that inherit from MonoBehavior and an interface (it's basically a class but it can't have variables and you can't define functions, everything 
 	that inherits/"implements" must define the functions in the interface) called SizeState.
 	Each script defines their own functionality in mechanics that differ between states. And the game calls the current state's function for that mechanic.
 	So tiny states eject function does nothing, while medium state's goes to tiny state, destroys the medium body, and jumps.
 	Right now Clayton has all 3 states scripts, and to control which one is the current I have a reference variable in Player Controller of the SizeState, and what ever one is current
 	is enabled, while the other states are disabled. To switch states I change the SizeState variable to whatever script it changes to, enables that script, and disables the old current script.

Also, the text that tells the player it can use a pickup is really buggy. If you can fix it go for it I have no idea why it's glitching.
 */
public class CameraController : MonoBehaviour {

    public GameObject player;

	// text that pops up when player is in range of picking up a pickup
	public static GameObject uiText;
	public GameObject uiTextPub;

    public Vector3 offset;

	public static bool isTriggered = false;

	void Start() {
		uiText = uiTextPub; // quick work-around to make the uitext static
	}

	void Update() {
		if (!isTriggered) // checks if the player is in range of any pickups, if it isn't it deactivates the text
			uiText.SetActive (false); 

		isTriggered = false; // resets to false so every frame there must be a pickup in range for the text to be displayed
	}
	
	// Update is called once per frame
	void LateUpdate () {

        transform.position = player.transform.position + offset; // just keeps the camera above the player
	}
}
