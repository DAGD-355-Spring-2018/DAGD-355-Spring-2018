 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

    public AudioClip jump;
	public AudioClip[] myLoop;
    public AudioClip playMusic;
	public AudioClip magic;

	public AudioSource source;
    public AudioSource music;
    public AudioSource sFX;
	public bool shouldPlay = false;
	private bool isPlaying = false;


	// Use this for initialization
	void Start () {
		source.clip = myLoop [2];
        music.volume = 0.6f;
        music.clip = playMusic;
        music.Play();
	}
	
	// Update is called once per frame
	void Update () {
        	
        if (shouldPlay && !isPlaying) {
                    source.loop = true;
                    //source.clip = myLoop [0];
                    source.Play ();
                    isPlaying = true;
        }

        if (shouldPlay == false)
        {
            source.Stop();
        }
         

            
            isPlaying = shouldPlay;
            
    }
    public void playMagic()
    {
		print("I'm doing something");
        sFX.Stop();
        sFX.clip = magic;
        sFX.Play();
    }

    public void switchTrack(int playerState) {
		source.Stop ();
		source.clip = myLoop[playerState];

			source.Play ();
	}
    public void playJump()
    {
        shouldPlay = true;
        sFX.Stop();
        sFX.clip = jump;
        sFX.Play();
        //shouldPlay = false;
    }
}

