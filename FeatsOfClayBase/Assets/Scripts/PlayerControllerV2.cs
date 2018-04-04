using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour {
	public GolemController gCon;
	public GameObject currentState;
	public GameObject stateHolder;
	public List<GameObject> bodyList;
	public float moveSpeed = 10;
	public float speed = 10;
	float moveX;
	float moveY;
	Rigidbody rb;

	public List<GameObject> nearbyInteractables;
	public GameObject closestInteractable;
	private Vector3 prevPos;
	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	

	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;

	float m_TurnAmount;
	float m_ForwardAmount;
	// Use this for initialization
	void Start () {

		if (Camera.main != null)
		{
			m_Cam = Camera.main.transform;
		}

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
	}
	void FixedUpdate()
	{
		//rb.AddForce(new Vector3(moveX,0, moveY) * speed); // still jank movement // read inputs
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		// calculate move direction to pass to character
		if (m_Cam != null)
		{
			// calculate camera relative direction to move:
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_Move = v * m_CamForward + h * m_Cam.right;
		}
		else
		{
			// we use world-relative directions in the case of no main camera
			m_Move = v * Vector3.forward + h * Vector3.right;
		}

		// pass all parameters to the character control script
		Move(m_Move);
		prevPos = transform.position;
	}

	public void Move(Vector3 move)
	{

		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();

		move = transform.InverseTransformDirection(move);
		m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = move.z;
		rb.velocity = new Vector3 (m_Move.x, rb.velocity.y, m_Move.z);
		//rb.AddForce(Physics.gravity*2);
		ApplyExtraTurnRotation();

		// control and velocity handling is different when grounded and airborne:
		
		

	}



	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
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

		transform.position = new Vector3(transform.position.x, transform.position.y + currentState.GetComponent<GolemController>().sState.height/2, transform.position.z);
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
		gCon.p = this;
	}
}
