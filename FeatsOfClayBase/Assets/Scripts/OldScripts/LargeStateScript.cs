using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeStateScript : MonoBehaviour, SizeState {
	// I don't plan on the body being like this, we can figure out how to represent it in Unity once we know what the body is and have it made up
	public Rigidbody rb; // rigid body of clayton
	public Rigidbody largeRb; // rigid body of the large body 
    public float jumpStrength;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
		if (largeRb) { // playing it safe
			float moveHorizontal = (Mathf.Abs (largeRb.velocity.x) < 2) ? Input.GetAxis ("Horizontal") : 0; // really bad movement controls
			float moveVertical = (Mathf.Abs (largeRb.velocity.z) < 2) ? Input.GetAxis ("Vertical") : 0; // im too lazy to make decent controls

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			largeRb.velocity += movement;

			rb.gameObject.transform.position = largeRb.gameObject.transform.position + offset; // keeps clayton floating above body
		}
    }

    public void onSmallPickup() { }

    public void onLargePickup() { }

    public void eject()
    {
        Destroy(largeRb.gameObject); // destroys the large body

        // shifts clayton up to 2 so it doesn't collide with the capsule before the offset is applied
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, 2, pos.z);


        PlayerController p = GetComponent<PlayerController>(); // reference to the player controller script
		MediumStateScript mss = GetComponent<MediumStateScript>(); // reference to the medium state script
        GameObject obj = Instantiate(p.medBody, pos, Quaternion.identity); // instantiates the medium body


        // enables the medium script
        mss.enabled = true; 
        mss.rb = rb; // gives the mss a reference to claytons rigidbody
        mss.medRb = obj.GetComponent<Rigidbody>(); // gives the mss a reference to the med body
		//mss.GetComponent<CapsuleCollider> ().isTrigger = true;

		mss.medRb.velocity += Vector3.up * (jumpStrength / 2); // jump

        Instantiate(p.largePickup, new Vector3(pos.x, .35f, pos.z), Quaternion.identity); // spawns a new large pickup below the player

		p.sizeState = (SizeState)mss; // changes the state to medium
		enabled = false; // disables this
    }
}
