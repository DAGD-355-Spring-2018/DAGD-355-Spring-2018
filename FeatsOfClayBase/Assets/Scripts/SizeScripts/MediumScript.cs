using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumScript : SizeScript {
	void Start()
	{
		height = .5f;
	}
	public override void Action()
	{
		Debug.Log("Medium Action");
	}
}
