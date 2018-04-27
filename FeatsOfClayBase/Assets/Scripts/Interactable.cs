using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public bool reflectLaser;
	public virtual void Interact(PlayerControllerV2 player)
	{
		Debug.Log("interact with me, senpai");
	}

	public virtual bool OnLaserHit()
	{
		return reflectLaser;
	}
}
