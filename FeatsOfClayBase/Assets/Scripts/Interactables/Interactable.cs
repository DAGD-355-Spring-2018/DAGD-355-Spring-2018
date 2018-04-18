using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public bool reflectLaser;
	public bool flamable;

	bool onFire = false;
	public virtual void Interact(PlayerControllerV2 player)
	{
		Debug.Log("interact with me, senpai");
	}

	public virtual bool OnLaserHit()
	{
		if(flamable)
		{
			OnFireHit();
		}
		return reflectLaser;
	}

	public virtual void OnFireHit()
	{
		if(flamable)
		{
			if(!onFire)
			{
				Instantiate(Camera.main.GetComponent<GameController>().particleSets[0],this.transform);
				onFire = true;
			}
			Debug.Log(gameObject.name + "Is on fire!");
		}
	}
}
