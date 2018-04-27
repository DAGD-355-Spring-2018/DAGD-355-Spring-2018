using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumStateScript : MonoBehaviour, SizeState {

	// I don't plan on the body being like this, we can figure out how to represent it in Unity once we know what the body is and have it made up
    public Rigidbody rb; // rigid body of clayton
    public Rigidbody medRb; // rigid body of the medium body 
    public float jumpStrength;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (medRb) { // playing it safe
	        float moveHorizontal = (Mathf.Abs(medRb.velocity.x) < 2) ? Input.GetAxis("Horizontal") : 0; // really bad movement controls
	        float moveVertical = (Mathf.Abs(medRb.velocity.z) < 2) ? Input.GetAxis("Vertical") : 0; // like really bad

	        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

	        medRb.velocity += movement; // just pretend this isn't here, someone figure out how to move something in Unity
	         
	        rb.gameObject.transform.position = medRb.gameObject.transform.position + offset; // keeps clayton floating above the body, somehow doesn't always work though?
		}
    }

    public void onSmallPickup() { } // doesn't do anything on the small pickup

    public void onLargePickup() // grows to large state
    {
        Destroy(medRb.gameObject); // destroys the medium body

        // shifts clayton up to 2 so it doesn't collide with the capsule before the offset is applied
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, 2, pos.z);


        PlayerController p = GetComponent<PlayerController>(); // reference to the player controller script
        LargeStateScript lss = GetComponent<LargeStateScript>(); // reference to the large state script
		pos.y += .3f; // don't feel like making a new vector for the next line
        GameObject obj = Instantiate(p.largeBody, pos, Quaternion.identity); // instantiates the large body at the players position

        // enables the large script
        lss.enabled = true;
        lss.rb = rb; // gives a reference to clayton's rigidbody, probably dont need to do this, could put a lot of this into an onEnable function
        lss.largeRb = obj.GetComponent<Rigidbody>(); // gives a reference to the large body's rigidbody
        p.sizeState = (SizeState)lss; // changes the state to large
        enabled = false; // disables this
    }

    public void eject()
	{
        Destroy(medRb.gameObject); // destroys the medium body
        GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(new Vector3(-30, 0, 0)); // rotates the camera down

		PlayerController p = GetComponent<PlayerController>(); // reference to the player controller script
		TinyStateScript tss = GetComponent<TinyStateScript>(); // reference to the tiny state script

        Vector3 pos = transform.position;
        //transform.position = new Vector3(pos.x, .5f, pos.z);

        tss.enabled = true; // enables tiny script
        p.sizeState = (SizeState)tss; // changes the state to tiny

		tss.rb.useGravity = true; // dont know why i ever had that there
		tss.rb.velocity += Vector3.up * (jumpStrength); // jump

		Instantiate(p.smallPickup, new Vector3(pos.x, .3f, pos.z), Quaternion.identity); // instantiates a new small pickup on the ground below the player

        enabled = false; // disables this
    }
}
