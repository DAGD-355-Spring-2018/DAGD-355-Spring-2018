using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	[Header("Elemental Attributes")]
	public bool reflectLaser;
	public bool flamable;
	public bool wet;
	public bool isDestroyedByFire;
	public float smoulderTimer = 1f;
	GameObject currentParticle;
	public float smoulderTimerDefault;
	public float burnTimer = 0;
	bool smouldering;
	
	public bool onFire = false;
	public virtual void Start()
	{
		smoulderTimerDefault = smoulderTimer;
	}
	public virtual void Interact(PlayerControllerV2 player)
	{
		Debug.Log("interact with me, senpai");
	}

	public virtual void OnWaterHit()
	{
		if (smouldering || onFire)
		{
			onFire = false;
			smouldering = false;
			smoulderTimer = smoulderTimerDefault;
			Destroy(currentParticle);
			currentParticle = null;
		}
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
		if(flamable && !onFire)
		{
			if(!smouldering)
			{
				smouldering = true;
			}
			if(smouldering)
			{
				smoulderTimer -= Time.deltaTime;
				if(smoulderTimer <=0)
				{
					smouldering = false;
					smoulderTimer = smoulderTimerDefault;
					onFire = true;
					currentParticle = Instantiate(Camera.main.GetComponent<GameController>().particleSets[0], this.transform);
					Debug.Log(gameObject.name + "Is on fire!");
				}
			}

		}
	}

	public virtual void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Interactable")
		{
			if(onFire)
			{
				col.gameObject.GetComponent<Interactable>().OnFireHit();
			}
			if(wet)
			{
				col.gameObject.GetComponent<Interactable>().OnWaterHit();
			}
		}
		else if (col.gameObject.tag == "Player")
		{
			if (onFire)
			{
				col.gameObject.GetComponent<PlayerControllerV2>().OnFireHit();
			}
			if (wet)
			{
				col.gameObject.GetComponent<PlayerControllerV2>().OnWaterHit();
			}
		}
	}
}
