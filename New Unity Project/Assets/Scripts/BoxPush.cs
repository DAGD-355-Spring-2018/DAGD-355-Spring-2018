using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 This is the block pushing script.  It should be attached to any prefab that will be pushable.
 This is written so far with the usage of physics to propel the object in a forward direction once the player
 collides with it (in the medium state ONLY) and pushes it for a set amount of time (representing effort and
 giving an illusion of weight to the object.

TODO: I want this to move forward on a single axis.  I also want it to stop after a specific distance is crossed.
      (As in, you move the box in increment.  Might be scrapped, but it helps with puzzles a lot if the player can
      judge how far the box will move and how many times it needs to be pushed).
      Check with the person making Clayton move.
      Add sound on push.
      Add animation for player pushing.
      Dust particles?
*/

[RequireComponent(typeof(Rigidbody))] // Rigibody element absolutely needed, this creates it if forgotten.

public class BoxPush : MonoBehaviour {

    public float objectMass = 300;  // Starting weight of the object
    public float pushAtMass = 100;  // Weight it must reach before being 'pushable'
    public float pushingTime; // Lets us vary the time it takes for the weight to diminish
    public float forceOfPush; // The amount of force applied on object
    Rigidbody rb;
    public float velocity; // For debug; lets you see velocity applied in the inspector

    private Vector3 dir;
    private Vector3 lastPos;
    private float basePushingTime = 0;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) return;  // It should be there, but just in case...

        rb.mass = objectMass; // 
	}

    // Is the box currently moving?
    bool IsMoving()
    {
        if (rb.velocity.magnitude > 0.06f)
        {
            return true;
        }
        return false;
    }

    // FixedUpdate is always the same time, and suited for physics calculation (i.e. Rigdibody stuff goes here)
    void FixedUpdate ()
    {
        // Lets use R to push, for the moment
        velocity = rb.velocity.magnitude;

        // Reset
        if (Input.GetKeyUp(KeyCode.R))
        {
            rb.isKinematic = true;
        }

        if (rb.isKinematic == false)
        {
            basePushingTime += Time.deltaTime;
            if (basePushingTime >= pushingTime)
            {
                basePushingTime = pushingTime;
            }

            // Linear interpolation to arrive at the desired mass in the desired time
            rb.mass = Mathf.Lerp(objectMass, pushAtMass, basePushingTime / pushingTime);
            // Adding the force/pushing
            rb.AddForce(dir * forceOfPush, ForceMode.Force);
        }
        else
        {
            // Reset
            rb.mass = objectMass;
            basePushingTime = 0;

        }
    }

    // Detecting Collision between the player's medium size and 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Medium")
        {

            if (Input.GetKey(KeyCode.R))
            {
                // Start the force countdown
                rb.isKinematic = false;

                // This works, but...  I'd rather the box's axis of movement be more constrained
                dir = collision.contacts[0].point - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;

            }
        }

    }
}
