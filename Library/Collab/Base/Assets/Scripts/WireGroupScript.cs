using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireGroupScript : MonoBehaviour {

	public bool on;
	Animator[] anims;

	public void turnOn(){
		on = true;
		for (int i = 0; i < anims.Length; i++) {
			anims[i].SetBool ("on", true);
		}
	}

	public void turnOff(){
		on = false;
		for (int i = 0; i < anims.Length; i++) {
			anims[i].SetBool ("on", false);
		}
	}

	// Use this for initialization
	void Start () {
		anims = transform.GetComponentsInChildren<Animator> ();
		if(on){
			turnOn ();
		}
	}

	public bool isPowered(){
		return on;
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
