using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emitAudio : MonoBehaviour {

    public AudioClip magic;
    public AudioSource lightSound;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void playMagic()
    {
        print("I'm doing something");
        lightSound.Stop();
        lightSound.clip = magic;
        lightSound.Play();
    }
}
