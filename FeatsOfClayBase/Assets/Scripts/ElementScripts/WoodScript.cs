using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : ElementScript {

	public override void Action()
	{
		Debug.Log("Wood Action");
	}

	public override bool OnLaserHit()
	{
		return base.OnLaserHit();
	}

	public override void OnFireHit()
	{
		base.OnFireHit();
	}
}
