using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyStateScript : MonoBehaviour, SizeState {

	public Rigidbody rb; // rigid body of clayton

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>(); // gets the rigidbody of clayton
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = (Mathf.Abs(rb.velocity.x) < 2) ? Input.GetAxis("Horizontal") : 0; // really bad controls for movement 
        float moveVertical = (Mathf.Abs(rb.velocity.z) < 2) ? Input.GetAxis("Vertical") : 0;
    
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity += movement;
    }

    public void onSmallPickup()
    {
        rb.useGravity = false; // stops using gravity so clayton floats

        // shifts clayton up to 2 so it doesn't collide with the capsule before the offset is applied
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, 2, pos.z);

        PlayerController p = GetComponent<PlayerController>(); // reference to player controller script
        MediumStateScript mss = GetComponent<MediumStateScript>(); // reference to medium state script
        GameObject obj = Instantiate(p.medBody, new Vector3(pos.x, 1, pos.z), Quaternion.identity);//, gameObject.transform);
        GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(new Vector3(30, 0, 0)); // rotates the camera down

        // enables the medium script
        mss.enabled = true;
        mss.rb = rb; // gives a reference to clayton
        mss.medRb = obj.GetComponent<Rigidbody>(); // gives a reference to the med body
        p.sizeState = (SizeState)mss; // changes the state to medium

        enabled = false; // disables this
    }

    public void onLargePickup() { } // does nothing

    public void eject() { } // does nothing
}
