using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : ElementScript {

	public override void Action()
	{
		Debug.Log("Crystal Action");
	}

	public override bool OnLaserHit()
	{
		return true;
	}
}
