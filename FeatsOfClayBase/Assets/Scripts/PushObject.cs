using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
 By Pascale Desjardins

 This is the block pushing script.  It should be attached to any prefab that will be pushable.
 Adds a FixedJoint on collision + button press; destroys it when button is released.
 While the FixedJoint exists, the box moves with Clayton.

TODO: I want it to stop after a specific distance is crossed. (As in, you move the box in increment.  Might be scrapped, but it helps with puzzles a lot if the player can
        judge how far the box will move and how many times it needs to be pushed).
      Add sound on push.
      Add animation for player pushing.
      Dust particles?
      Fix the collision NullReferenceException?  It's annoying. 
*/

[RequireComponent(typeof(Rigidbody))] // Rigibody element absolutely needed, this creates it if forgotten.

public class PushObject : Interactable
{
    Rigidbody boxRB;
    GameObject pushObj;
    public bool touching = false;
    public bool hasJoint = false;


    // Use this for initialization
    void Start()
    {
        boxRB = GetComponent<Rigidbody>();
        if (boxRB == null) return;  // It should be there, but just in case...

        //rb.mass = objectMass; // 

    }

    // Update is called once per frame
    void Update()
    {
		/*
        if (touching && Input.GetKey(KeyCode.R))//
        {
            boxRB.isKinematic = false;
            if (pushObj.gameObject.GetComponent<Rigidbody>() != null && !hasJoint)
            {
                gameObject.AddComponent<FixedJoint>();
                gameObject.GetComponent<FixedJoint>().connectedBody = pushObj.GetComponent<Rigidbody>();
                hasJoint = true;
            }
        }//

        // Reset
        if (!touching || Input.GetKeyUp(KeyCode.R))
        {
            boxRB.isKinematic = true;
            Destroy(GetComponent<FixedJoint>());
            hasJoint = false;
        }
		*/
    }

	public void changeState()
	{
		if (!hasJoint)
		{
			boxRB.isKinematic = false;
			hasJoint = true;
        }
		else if(hasJoint)
		{
			boxRB.isKinematic = true;
            Destroy(GetComponent<FixedJoint>());
            hasJoint = false;
		}
	}

    /*
    // Triggering the move prompt.  A text object should appear to say, 'hey, press (key) to move me!' or somesuch.
    // Text should disappear when player leaves.  Text should appear over box?
    private void OnTriggerEnter(Collider other)
    {
        pushObj = other.gameObject;

        if (pushObj.GetComponentInParent<PlayerControllerV2>().bodyList.Count == 2)
        {
            touching = true;
        }

        // Show the text.
    }*/
	/*
    private void OnTriggerExit(Collider other)
    { 
        pushObj = other.gameObject;

        if (pushObj.GetComponentInParent<PlayerControllerV2>().bodyList.Count == 2)
        {
            touching = false;
        }
        // Hide the text.
    }
	*/
    // Detecting the collision: Checking if Clayton is the right size, then if the player's pressing the button,
    //                          then moving the box.  Box stops moving if player isn't pressing button.
//    private void OnCollisionStay(Collision other)
//    {
//        pushObj = other.gameObject;
//
//        if (pushObj.GetComponentInParent<PlayerControllerV2>().bodyList.Count == 2)
//        {
//            Debug.Log("Medium Clayton Touched This, Anything Else Is WRONG");
//            touching = true;
//        }
//    }

//    private void OnCollisionExit(Collision other)
//    {
//        pushObj = other.gameObject;
//
//        if (pushObj.GetComponentInParent<PlayerControllerV2>().bodyList.Count == 2)
//        {
//            touching = false;
//        }
//    }

}