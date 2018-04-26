using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour {
	public GolemController gCon;
	public GameObject currentState;
	public GameObject stateHolder;
	public List<GameObject> bodyList;
	public float buoyancy = 10;
	public float moveSpeed = 10;
	public float speed = 10;
	public float waterMult = 1;
	public float fireMult = 2;
	public bool floating;

	bool InLaser;
	public Rigidbody rb;
	float moveX;
	float moveY;
	float forceFactor = 0f;
	float waterLevel = 0f;
	float laserTimer = 0.1f;
	public List<GameObject> nearbyInteractables;
	public GameObject closestInteractable;
	private Vector3 prevPos;
	private Transform m_Cam;                // A reference to the main camera in the scenes transform
	private MainCameraController mCC;
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;

	PlayAudio audioScript;


	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;

	float m_TurnAmount;
	float m_ForwardAmount;
	// Use this for initialization
	void Start() {
		if (Camera.main != null)
		{
			m_Cam = Camera.main.transform;
			mCC = m_Cam.GetComponent<MainCameraController>();
		}
		audioScript = GetComponent<PlayAudio>();
		rb = GetComponent<Rigidbody>();
		UpdateCurrentState();
	}

	// Update is called once per frame
	void Update()
	{
		if (!PauseMenu.GameIsPaused)
		{
			FindClosestInteractale();
			if (Input.GetButtonDown("Fire1")) // Generic action for element
			{
				gCon.eState.Action();
			}
			if (Input.GetButtonDown("Fire2")) // generic action for Size
			{
				gCon.sState.Action();
			}
			if (Input.GetButtonDown("Jump")) // Eject button
			{
				Eject();
			}
			if (gCon.eState.onFire)
			{
				gCon.eState.burnTimer -= Time.deltaTime;
				if (gCon.eState.burnTimer <= 0)
				{
					Eject();
				}
			}
		}
		laserTimer -= Time.deltaTime;
		if(InLaser)
		{
			laserTimer = 1f;
		}
		if(laserTimer < 0)
		{
			InLaser = false;
		}
	}
	void FixedUpdate()
	{
		if (!PauseMenu.GameIsPaused)
		{
			if (floating)
			{
				Float();
			}
			//rb.AddForce(new Vector3(moveX,0, moveY) * speed); // still jank movement // read inputs
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

			// Play audio clips for character movement
			if (h != 0 || v != 0)
			{
				audioScript.shouldPlay = true;
			}
			else
			{
				audioScript.shouldPlay = false;
			}
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
	}

	public void Move(Vector3 move)
	{

		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f || gCon.eState.onFire) move.Normalize();
		move = transform.InverseTransformDirection(move);
		m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = move.z;
		m_Move *= gCon.sState.speed;
		//state movement modifiers!
		if (gCon.eState.onFire)
		{
			m_Move *= fireMult;
		}
		rb.velocity = new Vector3(m_Move.x, rb.velocity.y, m_Move.z);
		//rb.AddForce(Physics.gravity*2);
		ApplyExtraTurnRotation();



	}

	public void FindClosestInteractale()
	{
		closestInteractable = null;
		float dis = 100; // set a arbitrary distance
		float tempDis = 0;
		closestInteractable = null;
		foreach (GameObject go in nearbyInteractables)
		{
			tempDis = Vector3.Distance(transform.position, go.transform.position);
			if (tempDis < dis)
			{
				closestInteractable = go; // if it is closer than the closest object previously, set it to the closest object.
				dis = tempDis; //  resetting the distance variable.
			}
		}
	}

	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}

	public void OnCollisionStay(Collision c)
	{
		if (c.transform.tag == "Interactable")
		{
			//Debug.Log("touching interactable");
			if (Input.GetButton("Fire3"))
			{
				c.transform.GetComponent<Interactable>().Interact(this);
				//Debug.Log("pickup");
			}
		}

	}
	// detecting all of the objects Clayton is touching that can be interacted with.
	public void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.GetComponent<Interactable>() != null)
		{
			if (!nearbyInteractables.Contains(c.gameObject))
			{
				nearbyInteractables.Add(c.gameObject);
			}
		}
	}

	public void OnCollisionExit(Collision c)
	{
		if (c.gameObject.GetComponent<Interactable>() != null)
		{
			if (nearbyInteractables.Contains(c.gameObject))
			{
				nearbyInteractables.Remove(c.gameObject);
			}

		}
	}

	public void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.layer == 4) //  layer 4 is water
		{
			OnWaterHit();
			if (gCon.eState.floats)
			{
				floating = true;
				
				waterLevel = c.gameObject.transform.position.y;
			}
		}
	}

	public void OnTriggerExit(Collider c)
	{
		if (c.gameObject.layer == 4) //  layer 4 is water
		{
			floating = false;
		}
	}
	public void PickupBody(GameObject go, int size) // adding the powerup from the body pickup to the player
	{
		bool fireTransfer = gCon.eState.onFire;
		GameObject tempGO = Instantiate(go, transform.position, transform.rotation, transform);
		bodyList.Add(tempGO);                           //Add it to the end of the body list
		tempGO.transform.parent = stateHolder.transform;
		UpdateCurrentState(); // makes sure that all of the body states are correct
		if (fireTransfer)
		{
			if (gCon.eState.flamable)
			{
				gCon.eState.BurstIntoFlames();
			}
		}
		transform.position = new Vector3(transform.position.x, transform.position.y + currentState.GetComponent<GolemController>().sState.height / 2, transform.position.z);
	}

	public void Eject()
	{
		bool fireTransfer = gCon.eState.onFire;
		floating = false;
		gCon.Eject(); // spawns the pickup on the ground
		rb.velocity += Vector3.up * gCon.sState.ejectStrength; // Each size state has a eject strength variable that can be used to make adjust the jump power of clayton by size
		if (bodyList.Count > 1) // if Clayton is not in the small state, remove the last object in clayton's body list
		{
			bodyList.RemoveAt(bodyList.Count - 1);
			Destroy(currentState);
			UpdateCurrentState();
			audioScript.playJump();
		}
		if (fireTransfer && gCon.eState.flamable)
		{
			gCon.eState.BurstIntoFlames();
		}
	}

	public void UpdateCurrentState()
	{
		currentState = bodyList[bodyList.Count - 1]; // Sets clayton's current state to the last object in the bodycount list
		gCon = currentState.GetComponent<GolemController>(); // Sets the current Golem controller to that body part's GolemController.
		gCon.p = this;
		SwitchAudioClip(bodyList.Count - 1);
		// used for updating the camera's distance
		mCC.target = currentState.transform;
		mCC.DistanceUp = gCon.sState.DistanceUp;
		mCC.maxDistance = gCon.sState.maxDistance;
		mCC.minDistance = gCon.sState.minDistance;
	}
	public void Float ()
	{
		float forceFactor = 1f - ((transform.position.y - waterLevel) / 3);
		if (forceFactor > 0f)
		{
			rb.AddForce(-Physics.gravity * (forceFactor - rb.velocity.y * buoyancy));
		}
	}

	public void SwitchAudioClip(int stateSize)
	{

		audioScript.switchTrack(stateSize);
	}

	// ElementalHits
	public void OnFireHit()
	{
		gCon.eState.OnFireHit();
	}
	public void OnWaterHit()
	{
		gCon.eState.OnWaterHit();
	}
	public bool OnLaserHit()
	{
		Debug.Log("Player hit by laser");
		if(gCon.eState.OnLaserHit())
		{
			audioScript.playMagic();
			InLaser = true;
		}
		return gCon.eState.OnLaserHit();
	}
}
