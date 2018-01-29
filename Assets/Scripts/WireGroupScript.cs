using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireGroupScript : MonoBehaviour {

	public bool on;
	Animator[] anims;
	SpriteRenderer[] spriteRenderers;
	public GameObject[] powerDependencies;
	public CenterPieceScript centrePiece = null;

	public void turnOn(){
		on = true;
	
		for (int i = 0; i < anims.Length; i++) {
			anims[i].SetBool ("on", true);
		}
		for (int i = 0; i < spriteRenderers.Length; i++) {
				spriteRenderers [i].color = new Color (1, 1, 1);
		}

		if (centrePiece!=null) {
			centrePiece.updatePowerSource ();
		}
	}
		
	public void turnOff(){
		on = false;

		if (centrePiece!=null) {
			centrePiece.updatePowerSource ();
		}
		for (int i = 0; i < anims.Length; i++) {
			anims[i].SetBool ("on", false);
		}
		for (int i = 0; i < spriteRenderers.Length; i++) {
				spriteRenderers [i].color = new Color (0.5f, 0.5f, 0.5f);
		}
	}

	public void checkPower(){
		bool recievingPower = false;
		for(int i = 0; i < powerDependencies.Length; i++){
			if(powerDependencies[i].GetComponent<WireGroupScript>().isPowered()){
				recievingPower = true;
			}
		}

		if(on && !recievingPower){
			turnOff();
		}
		if(!on && recievingPower){
			turnOn ();
		}
	}

	// Use this for initialization
	void Start () {
		anims = transform.GetComponentsInChildren<Animator> ();
		spriteRenderers = Components.GetComponentsInDirectChildren<SpriteRenderer> (transform);
		if (on) {
			turnOn ();
		} else {
			turnOff ();
		}
	}

	public bool isADependantOf(GameObject obj){
		for(int i = 0; i<powerDependencies.Length; i++){
			if(obj == powerDependencies[i]){
				Debug.Log ("IS A DEPENDANCY");
				return true;
			}
		}
		Debug.Log ("ISNOT A DEPENDANCY");
		return false;
	}

	public bool isPowered(){
		return on;
	}
		
	// Update is called once per frame
	void Update () {
		bool recievingPower = false;
		if(powerDependencies.Length > 0){ // This is only here so it doesnt fuck up places where powerDependancies isnt implimented yet
			for(int i = 0; i < powerDependencies.Length; i++){
				if (powerDependencies [i].tag == "wire") {
					if(powerDependencies[i].GetComponent<WireGroupScript>().isPowered()){
						recievingPower = true;
					}
				}
				if (powerDependencies [i].tag == "Spiral") {
					if (powerDependencies [i].GetComponent<SpiralWheelSciript> ().isConnectedToThisObj (this.gameObject) &&
					   powerDependencies [i].GetComponent<SpiralWheelSciript> ().checkIfBeingPowered ()) {
						recievingPower = true;
					}
					
				}
				if (powerDependencies [i].tag == "Switch" && powerDependencies [i].GetComponent<SwitchScript> ().isPowered () &&
					powerDependencies [i].GetComponent<SwitchScript> ().isOn ()){
					recievingPower = true;
				}
			}
			if(recievingPower){
				turnOn ();
			}
			else{
				turnOff ();
			}
		}
	}
}

public static class Components{
	public static T[] GetComponentsInDirectChildren<T>(this Component parent) where T : Component
	{
		List<T> tmpList = new List<T>();

		foreach (Transform transform in parent.transform)
		{
			T component;
			if ((component = transform.GetComponent<T>()) != null)
			{
				tmpList.Add(component);
			}
		}

		return tmpList.ToArray();
	}
}
