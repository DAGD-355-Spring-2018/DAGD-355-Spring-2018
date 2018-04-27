using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxScript :ElementScript {

	public override void Action()
	{
		Debug.Log("Wax Action");
	}

	public override void onFireHit(PlayerControllerV2 pc) {
		pc.setOnFire ();
	}
}
