using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript :ElementScript {

	public GameObject rock;
	public float rockSpeed;
	public override void Action()
	{
		GameObject thrownRock = Instantiate(rock, transform.position, transform.rotation);
		thrownRock.GetComponent<Rigidbody>().AddForce(thrownRock.transform.forward * rockSpeed);
	}
	public override void Eject()
	{
		Debug.Log("Stone Eject");
		base.Eject();
	}
}
