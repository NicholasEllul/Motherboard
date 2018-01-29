using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	private AudioSource audioPlayer;
	public AudioClip[] musicTracks; 
	private int counterIndex = 0;
	// Use this for initialization
	void Start () {
		audioPlayer = GetComponent<AudioSource> ();
		audioPlayer.PlayOneShot (musicTracks [0]);
	}
	
	// Update is called once per frame
	void Update () {
		if(audioPlayer.isPlaying == false){
			counterIndex++;
			audioPlayer.PlayOneShot(musicTracks[counterIndex%musicTracks.Length]);
		}
	}
}
