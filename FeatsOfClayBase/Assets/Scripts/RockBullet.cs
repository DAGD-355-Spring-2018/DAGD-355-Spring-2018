using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBullet :Interactable {
	Rigidbody rb;
	public float despawnTimer;
	public float speedTheshold;
	public float velocityChecker;
	// Use this for initialization
	public override void Start () {
		rb = GetComponent<Rigidbody>();
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		velocityChecker = rb.velocity.magnitude;
		if(rb.velocity.magnitude > speedTheshold)
		{
			thrown = true;
		}
		else
		{
			thrown = false;
			despawnTimer -= Time.deltaTime;
		}
		if(despawnTimer <= 0)
		{
			Destroy(gameObject);
		}
	}
}
