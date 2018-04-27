using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.CompareTag ("Player")) {
			PlayerControllerV2 pc = c.gameObject.GetComponentInParent<PlayerControllerV2> ();
			pc.gCon.eState.onFireHit (pc);
		}
	}
}
