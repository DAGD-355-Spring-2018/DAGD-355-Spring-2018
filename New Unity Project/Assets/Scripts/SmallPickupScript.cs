using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallPickupScript : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Awake () {
        player = GameObject.Find("Clayton");
    }

    private void OnTriggerEnter(Collider other)
    {
		onTrigger ();
    }

	void OnTriggerStay(Collider other)
	{
		onTrigger (); // does the same as on enter
	}

    private void OnTriggerExit(Collider other)
    {
		//CameraController.isTriggered = false; // commented this out, this isn't necessary because of how this is set up
    }

	private void onTrigger()
	{
		bool isEligible = player.GetComponent<TinyStateScript>().enabled; // checks if it is in the tiny state, medium and large cant pick this up
		if(isEligible) // if its tiny
		{
			CameraController.uiText.SetActive (true);
			CameraController.isTriggered = true; // tells camera controller that there is atleast 1 pickup that is in range of the player
			if (Input.GetKeyDown(KeyCode.E)) // if the player presses e
			{
				player.GetComponent<PlayerController>().sizeState.onSmallPickup(); // calls the tiny states on small pickup function (changes into medium state)
				Destroy(gameObject);  // destroys this gameobject
			}
		}
	}
}
