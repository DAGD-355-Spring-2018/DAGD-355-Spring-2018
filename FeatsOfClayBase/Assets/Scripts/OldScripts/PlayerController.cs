using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    public GameObject medBody; // prefab for med body
    public GameObject largeBody; // prefab for large body
    public GameObject smallPickup; // prefab for small pickup
	public GameObject largePickup; // prefab for large pickup

    static public bool isGrounded; // dumb way of controlling physics, this is just for the eject/growing phases, dont use these movement and physics controls, they are garbage
    public float speed;
    public float jumpStrength;

	public SizeState sizeState; // What state is currently active (TinyStateScript, MediumStateScript, or LargeStateScript)

    private void Start()
    {
        sizeState = GetComponent<TinyStateScript>();
        GetComponent<MediumStateScript>().enabled = false;
        GetComponent<LargeStateScript>().enabled = false;
    }
    private void Update() // could probably have an if else chain here to see what state its in to figure out what physics body to move
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sizeState.eject(); // if the space bar is pressed, call the states eject function
        }
    }
}
