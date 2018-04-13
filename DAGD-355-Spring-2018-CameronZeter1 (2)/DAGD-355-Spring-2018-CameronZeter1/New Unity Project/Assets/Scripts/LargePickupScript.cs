using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Planning on deleting this and the SmallPickupScript and making just one with a boolean variable to tell what is large and what is small

public class LargePickupScript : MonoBehaviour{

    private GameObject player;
    private GameObject rotator;
    Vector3 rayDirection;

    // Use this for initialization
    void Awake()
    {
		player = GameObject.Find("Clayton");
        rotator = GameObject.Find("PlayerRotator");
        transform.forward = rotator.transform.TransformDirection(Vector3.forward);
        //Debug.Log (player);
    }

    private void OnTriggerEnter(Collider other)
    {
		onTrigger ();
	}

	void OnTriggerStay(Collider other)
	{
		onTrigger ();
	}

	private void OnTriggerExit(Collider other)
	{
		//CameraController.isTriggered = false; // commented this out, this isn't necessary because of how this is set up
	}

	private void onTrigger()
	{
		bool isEligible = player.GetComponent<MediumStateScript>().enabled;// checks if it is in the medium state, small and large cant pick this up
		if(isEligible) // if its medium
		{
			CameraController.uiText.SetActive (true); 
			CameraController.isTriggered = true; // tells camera controller that there is atleast 1 pickup that is in range of the player
			if (Input.GetKeyDown(KeyCode.E))
			{
				player.GetComponent<PlayerController>().sizeState.onLargePickup(); // calls the medium states on large pickup function (changes into large state)
				Destroy(gameObject);  // destroys this gameobject
			}
		}
	}
}
