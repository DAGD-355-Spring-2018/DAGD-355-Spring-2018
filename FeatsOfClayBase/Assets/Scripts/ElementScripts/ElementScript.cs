using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementScript : MonoBehaviour {
	[Header("Elemental Attributes")]
	public bool reflectLaser;
	public bool flamable;
	public bool isDestroyedByFire;

	public float smoulderTimer = 1f;
	GameObject currentParticle;
	public float smoulderTimerDefault;
	public float burnTimer = 0;
	public float burnTimerDefault;
	public bool smouldering;
	public bool onFire = false;

	public virtual void Start()
	{
		smoulderTimerDefault = smoulderTimer;
		burnTimerDefault = burnTimer;
	}
	public virtual void Action()
	{
		Debug.Log("Element Action");
	}
	public virtual void Eject()
	{
		Debug.Log("Elemental Eject");
	}

	public virtual bool OnLaserHit()
	{
		Debug.Log("His by Laser!");
		if (flamable)
		{
			OnFireHit();
		}
		return reflectLaser;
	}

	public virtual void OnFireHit()
	{
		Debug.Log("His by Fire!");
		if (flamable && !onFire)
		{
			if (!smouldering)
			{
				smouldering = true;
			}
			if (smouldering)
			{
				smoulderTimer -= Time.deltaTime;
				if (smoulderTimer <= 0)
				{
					BurstIntoFlames();
					
				}
			}

		}
	}
	public virtual void BurstIntoFlames()
	{
		smouldering = false;
		smoulderTimer = smoulderTimerDefault;
		onFire = true;
		currentParticle = Instantiate(Camera.main.GetComponent<GameController>().particleSets[0], this.transform);
		Debug.Log(gameObject.name + "Is on fire!");
	}
	public virtual void OnWaterHit()
	{
		if (smouldering || onFire)
		{
			onFire = false;
			smouldering = false;
			smoulderTimer = smoulderTimerDefault;
			burnTimer = burnTimerDefault;
			Destroy(currentParticle);
			currentParticle = null;
		}
	}
}
