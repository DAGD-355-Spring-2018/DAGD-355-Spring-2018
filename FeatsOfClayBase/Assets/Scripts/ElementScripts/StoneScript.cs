using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript :ElementScript {

	public override void Action()
	{
		Debug.Log("Stone Action");
	}
	public override void Eject()
	{
		Debug.Log("Stone Eject");
		base.Eject();
	}
}
