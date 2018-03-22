using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour {
	public GolemController gCon;
	public GameObject currentState;
	public GameObject stateHolder;
	public List<GameObject> bodyList;
	public float speed = 10;
	float moveX;
	float moveY;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		UpdateCurrentState();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) // Generic action for element
		{
			gCon.eState.Action();
		}
		if (Input.GetButtonDown("Fire2")) // generic action for Size
		{
			gCon.sState.Action();
		}
		if(Input.GetButtonDown("Jump")) // Eject button
		{
			Eject();
		}
		// Boring movement. Realstically we should have different movement code for each size, or a more advanced movement system.
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");
	}
	void FixedUpdate()
	{
		rb.AddForce(new Vector3(moveX,0, moveY) * speed); // still jank movement
	}
	public void OnCollisionStay(Collision c)
	{
		if(c.transform.tag == "Interactable")
		{
			//Debug.Log("touching interactable");
			if (Input.GetButton("Fire3"))
			{
				c.transform.GetComponent<Interactable>().Interact(this);
				//Debug.Log("pickup");
			}
		}

	}

	public void PickupBody(GameObject go, int size) // adding the powerup from the body pickup to the player
	{
		GameObject tempGO = Instantiate(go, transform.position, transform.rotation, transform);
		bodyList.Add(tempGO);							//Add it to the end of the body list
		tempGO.transform.parent = stateHolder.transform; 
		UpdateCurrentState(); // makes sure that all of the body states are correct
	}
	public void Eject()
	{
		gCon.Eject(); // spawns the pickup on the ground
		rb.velocity += Vector3.up * gCon.sState.ejectStrength; // Each size state has a eject strength variable that can be used to make adjust the jump power of clayton by size
		if (bodyList.Count > 1) // if Clayton is not in the small state, remove the last object in clayton's body list
		{
			bodyList.RemoveAt(bodyList.Count - 1);
			Destroy(currentState);
			UpdateCurrentState();
		}
	}

	public void UpdateCurrentState()
	{
		currentState = bodyList[bodyList.Count - 1]; // Sets clayton's current state to the last object in the bodycount list
		gCon = currentState.GetComponent<GolemController>(); // Sets the current Golem controller to that body part's GolemController.
	}
}
