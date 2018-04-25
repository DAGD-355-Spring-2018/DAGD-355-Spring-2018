using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeScript : SizeScript {
	void Start()
	{
		height = 1f;
	}
	public override void Action()
	{
		Debug.Log("Large Action");
	}
}
