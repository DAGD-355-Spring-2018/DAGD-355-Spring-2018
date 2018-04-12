﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementScript : MonoBehaviour {

	public virtual void Action()
	{
		Debug.Log("Element Action");
	}
	public virtual void Eject()
	{
		Debug.Log("Elemental Eject");
	}
}
