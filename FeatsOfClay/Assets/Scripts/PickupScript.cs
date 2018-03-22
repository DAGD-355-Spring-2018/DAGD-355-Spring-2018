using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : Interactable {
	public GameObject gBody;
	public int size;

	public void Start()
	{
		//Debug.Log("Spawned interactable");
		DisablePlayerCollision();
		
	}
	public override void Interact(PlayerControllerV2 player) // we need to check if the player's state is correct, but this is the general idea.
	{
		if (player.bodyList.Count == size - 1)
		{
			player.PickupBody(gBody, size);
			Destroy(this.gameObject);
		}
		//Debug.Log("Interacted with a pickup");
	}

	public void DisablePlayerCollision()
	{
		foreach (Transform child in transform)
		{
			child.gameObject.layer = 9;
		}
		gameObject.layer = 9;
		print("collision disabled");
		Invoke("ReEnablePlayerCollision", 0.5f);
	}
	void ReEnablePlayerCollision()
	{
		foreach (Transform child in transform)
		{
			child.gameObject.layer = 0;
		}
		gameObject.layer = 0;
		Debug.Log("collision reEnabled");
	}
}
