using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : Interactable {
	public GameObject gBody;
	public int size;
	public int element;
	// 0 = stone
	// 1 = crystal

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
			player.nearbyInteractables.Remove(this.gameObject);
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
		Invoke("ReEnablePlayerCollision", 0.2f);
	}
	void ReEnablePlayerCollision()
	{
		foreach (Transform child in transform)
		{
			child.gameObject.layer = 11;
			child.tag = "Interactable";
		}
		gameObject.layer = 11;
		Debug.Log("collision reEnabled");
	}
}
