using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    public Transform trail;
    public BoxCollider water;
    private bool waxState = true;
    public Rigidbody rb;
    public float thrust;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Player is within trigger");
        rb.AddRelativeForce(Vector3.forward * thrust);
    }

    // Use this for initialization
    void Start() {
        water = water.GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (waxState == true)
        {
            trail.GetComponent<ParticleSystem>().enableEmission = true;
            water.size = new Vector3(1, 1, 1);
            water.center = new Vector3(0, 0, 0);
        }

        if (waxState == false)
        {
            trail.GetComponent<ParticleSystem>().enableEmission = false;

            water.size = new Vector3 (1, 20, 1);
            water.center = new Vector3(0, 0, 0);
        }

    }
}
