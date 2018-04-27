using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject clayton;
	public MainCameraController mCC;
	static GameObject player;
	// Use this for initialization
	void Start () {
		if (player == null)
		{
			player = clayton;
		}
		mCC = GetComponent<MainCameraController>();
		mCC.target = player.GetComponent<PlayerControllerV2>().currentState.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
