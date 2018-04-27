using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeScript : SizeScript {
	void Start()
	{
		height = 1f;
		burnTime = 10f;
	}
	public override void Action()
	{
		Debug.Log("Large Action");
	}
}
