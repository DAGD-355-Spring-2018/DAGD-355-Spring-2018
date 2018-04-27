using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : ElementScript {

	public override void onFireHit(PlayerControllerV2 pc) {
		pc.setOnFire ();
	}
}
