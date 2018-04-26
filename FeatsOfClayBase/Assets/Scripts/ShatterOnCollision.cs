using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterOnCollision : MonoBehaviour {

	public GameObject replacement;

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Interactable")
		{
			if (col.gameObject.GetComponent<Interactable>().thrown == true)
			{
				GameObject.Instantiate(replacement, transform.position, transform.rotation);

				Destroy(gameObject);
			}
		}
	}
}
