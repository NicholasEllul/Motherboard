using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour{


	public bool switchOn;
	Animator anim;
	public GameObject[] dependencies;
	public WireGroupScript[] wires;
	ParticleSystem ps;
	bool powered = false;

	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator>();
		ps = GetComponentInChildren<ParticleSystem> ();
		if(switchOn){
			anim.SetBool("switchOn", true);
			ps.Play ();

			for (int i = 0; i < wires.Length; i++) {
				wires [i].turnOn();
			}
		}
	}


	// Update is called once per frame
	void Update () {
		if (dependencies.Length > 0) {
			bool isBeingPowered = false;
			for (int i = 0; i < dependencies.Length; i++) {
				if (dependencies [i].tag == "wire" && dependencies[i].GetComponent<WireGroupScript> ().isPowered()) {
					isBeingPowered = true; 
				}
			}
			powered = isBeingPowered;
			if (!powered && ps.isPlaying) {
				ps.Stop ();
			} else if(powered && ps.isStopped && switchOn){
				ps.Play ();
			}
		}
	}

	public bool isPowered(){
		return powered;
	}
	public bool isOn(){
		return switchOn;
	}

	public void hit(){
		if (switchOn) {
			switchOn = false;
			anim.SetBool("switchOn", false);

			ps.Stop ();

			for (int i = 0; i < wires.Length; i++) {
				wires [i].turnOff();
			}
		} else {
			switchOn = true;
			anim.SetBool ("switchOn", true);
			ps.Play ();

			for (int i = 0; i < wires.Length; i++) {
				wires [i].turnOn();
			}
		}
	}
}
