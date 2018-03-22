using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject clayton;
	static GameObject player;
	// Use this for initialization
	void Start () {
		if (player == null)
		{
			player = clayton;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
