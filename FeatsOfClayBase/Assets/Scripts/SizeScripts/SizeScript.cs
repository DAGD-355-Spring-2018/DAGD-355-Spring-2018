using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeScript : MonoBehaviour {
	public bool dropsPickup = true;
	public float ejectStrength = 0;
	public float height = 0;
	public float speed = 1;

	public float DistanceUp = 0;
	public float minDistance = 0;
	public float maxDistance = 0;
	public GolemController g;
	public virtual void Action()
	{
		Debug.Log("sizeAction");
	}
	public virtual void Eject()
	{
		Debug.Log("Medium Eject");
	}
}
