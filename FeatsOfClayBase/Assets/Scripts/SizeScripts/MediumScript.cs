using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumScript : SizeScript {
	PushObject pushBlock;
	void Start()
	{
		height = .5f;
		burnTime = 4f;
	}
	public override void Eject()
	{
		DestroyJoint();
		base.Eject();
	}
	public override void Action() // This shit will pick stuff up!
	{
		if(pushBlock == null)
		{
			if (g.p.closestInteractable != null)
			{
				Debug.Log("close Interactable Exists");
				pushBlock = g.p.closestInteractable.GetComponent<PushObject>();
				if (pushBlock != null)
				{
					Debug.Log("Closest Object is a Push Block");
					if (!pushBlock.hasJoint)
					{
						CreateJoint();
					}
					
				}
			}	
		}
		else
		{
			DestroyJoint();
		}
		Debug.Log("Medium Action");
	}

	private void CreateJoint()
	{
		Debug.Log("Creating Joint");
		pushBlock.gameObject.AddComponent<FixedJoint>();
		pushBlock.GetComponent<FixedJoint>().connectedBody = g.p.rb;
		pushBlock.ChangeState();
	}
	private void DestroyJoint()
	{
		if (pushBlock != null)
		{
			Debug.Log("removing Joint");
			pushBlock.ChangeState();
			pushBlock = null;
		}
	}
}
