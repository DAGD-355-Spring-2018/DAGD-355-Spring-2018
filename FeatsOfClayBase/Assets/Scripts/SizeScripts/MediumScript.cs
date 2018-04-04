using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumScript : SizeScript {
	void Start()
	{
		height = .5f;
	}
	public override void Action() // This shit will pick stuff up!
	{
		if(g.p.closestInteractable != null)
		{
			PushObject pushBlock = g.p.closestInteractable.GetComponent<PushObject>();
			if(pushBlock != null)
			{
				if(!pushBlock.hasJoint)
				{
					pushBlock.gameObject.AddComponent<FixedJoint>();
					pushBlock.GetComponent<FixedJoint>().connectedBody = GetComponent<Rigidbody>();
					pushBlock.changeState();
				}
				else
				{
					pushBlock.changeState();
				}
			}
				
		}
		Debug.Log("Medium Action");
	}
}
