﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeScript : MonoBehaviour {
	public bool dropsPickup = true;
	public float ejectStrength = 0;
	public float height = 0;
	public GolemController g;
	public virtual void Action()
	{
		Debug.Log("sizeAction");
	}
}
