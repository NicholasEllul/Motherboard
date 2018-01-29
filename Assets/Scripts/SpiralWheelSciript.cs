using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWheelSciript : MonoBehaviour {

	public enum Direction {BOTTOM = 0, RIGHT = 1, TOP = 2, LEFT = 3};

	private const float degreesPerRotation = 200;
	public GameObject[] inputWires;				// Wires coming into the spiral. Is parrallel to the directions array.
	public GameObject wireOnSpiral;				// Gameobject for the wire grouping on the spiral
	public Direction[] directionWiresComingFrom; // Where relative to the center of the circle are wires coming into the spiral. Is parrallel to the input array.
	public Direction[] wiresOnSpiralDirection; // Directions that the spinning wires face when rotation is 0
	public GameObject[] potentialPowerSources;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void hit(){
		// HIt logic
		rotate();
	}

	public bool isConnectedToThisObj (GameObject obj){
		for(int i = 0; i < inputWires.Length; i++){
			if(inputWires[i] == obj){
				for(int j = 0; j < wiresOnSpiralDirection.Length; j++){
					if(wiresOnSpiralDirection[j] == directionWiresComingFrom[i]){
						return true;
					}
				}
			}
		}
		return false;
	}

	public bool checkIfBeingPowered(){
		bool powered = false;

		for(int i = 0; i < inputWires.Length; i++){
			if(inputWires[i].GetComponent<WireGroupScript>().isPowered()){
				for(int j = 0; j < wiresOnSpiralDirection.Length; j++){
					if((int)directionWiresComingFrom[i] == (int)wiresOnSpiralDirection[j] && !inputWires[i].GetComponent<WireGroupScript>().isADependantOf(this.gameObject)){
						powered = true;
					}
				}
			}
		}
		return powered;
	}
		
	/*private void powerConnections (){
		
		for(int i = 0; i <wiresOnSpiralDirection.Length; i ++){
			for(int j = 0; j < directionWiresComingFrom.Length; j++){
				if(directionWiresComingFrom[j] == wiresOnSpiralDirection[i]){
						inputWires[j].GetComponent<WireGroupScript> ().turnOn ();
				}
			}
		}
	}
*/
	public void rotate(){

		transform.Rotate (0, 0, 0);
		for(int i = 0; i < wiresOnSpiralDirection.Length; i ++){
			wiresOnSpiralDirection[i] = (Direction)(((int)wiresOnSpiralDirection[i] + 1) % 4);
		}
		bool isPowered = checkIfBeingPowered();
		if(isPowered){
			wireOnSpiral.GetComponent<WireGroupScript> ().turnOn ();
		//	powerConnections();
		}
		else{
			wireOnSpiral.GetComponent<WireGroupScript> ().turnOff ();
		}
			
		anim.SetTrigger ("rotate");

	}

	// Update is called once per frame
	void Update () { 

		if(checkIfBeingPowered()){
			wireOnSpiral.GetComponent<WireGroupScript> ().turnOn ();
		//	powerConnections();
			Debug.Log("We have power...");
		}
		else{
			wireOnSpiral.GetComponent<WireGroupScript> ().turnOff ();
		}
	}
}
