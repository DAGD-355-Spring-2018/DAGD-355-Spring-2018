using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : MonoBehaviour {
	public ElementScript eState;
	public SizeScript sState;

	public GameObject pickup;
	
	public PlayerControllerV2 p;

	public void Eject()
	{
		sState.Eject();
		eState.Eject();
		if (sState.dropsPickup && !eState.onFire)
		{
			GameObject newPickup = Instantiate(pickup, transform.position, transform.rotation, null);
			Debug.Log("dropping Pickup");
		}

		return;
	}
}
