using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour {

	public Transform player;
	public Transform playerCam;
	public float throwForce = 10; //Determines force of throw
	bool hasPlayer = false;
	bool beingCarried = false;
	public int dmg;
	private bool touched = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float dist = Vector3.Distance (gameObject.transform.position, player.position);
		if (dist <= 2.5f) {

			hasPlayer = true;
		} else {

			hasPlayer = false;
		}
		//Pickup of object
		if (hasPlayer && Input.GetButtonDown ("use")) {
			GetComponent<Rigidbody> ().isKinematic = true;
			transform.parent = playerCam;
			beingCarried = true;
		}
		//Setting variables for when it is being carried
		if (beingCarried) {
			if (touched) {
				GetComponent<Rigidbody> ().isKinematic = false;
				transform.parent = null;
				beingCarried = false;
				touched = false;
			}
			// Throw function- Left click to throw- Right click to drop
			if (Input.GetMouseButtonDown (0)) {
				GetComponent<Rigidbody> ().isKinematic = false;
				transform.parent = null;
				beingCarried = false;
				GetComponent<Rigidbody> ().AddForce (playerCam.forward * throwForce);
			} else if (Input.GetMouseButtonDown (1)) {
				GetComponent<Rigidbody> ().isKinematic = false;
				transform.parent = null;
				beingCarried = false;
			}
		}
		
	}
	//Deals with second collider on object to make object fall if it hits something else when being carried
	void OnTriggerEnter()
	{
		if (beingCarried) {
			touched = true;
		}
	}
}
