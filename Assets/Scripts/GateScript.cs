using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

	public enum GateType {AND, OR, NAND, XOR};  

	public GateType typeOfGate;
	public GameObject[] inputWires;
	public GameObject outputWire;
	private bool isLettingPowerThrough;


	// Use this for initialization

	void Start () {
		
	}

	private bool andGateCheck(){
		bool poweredProperly = true;
		for(int i = 0; i < inputWires.Length; i++){
			if(inputWires[i].GetComponent<WireGroupScript>().isPowered() == false){
				return false;
			}
		}
		return poweredProperly;
	}

	private bool xorGateCheck(){
		int poweredWires = 0;
		for(int i = 0; i < inputWires.Length; i++){
			if(inputWires[i].GetComponent<WireGroupScript>().isPowered()){
				poweredWires++;
			}
		}
		if(poweredWires == 1){
			return true;
		}
		return false;
	}
		
	private bool nandGateCheck(){
		bool poweredProperly = false;
		for(int i = 0; i < inputWires.Length; i++){
			if(inputWires[i].GetComponent<WireGroupScript>().isPowered() == false){
				poweredProperly = true;
			}
		}
		return poweredProperly;
	}

	public bool orGateCheck(){
		bool poweredProperly = false;
		for(int i = 0; i < inputWires.Length; i++){
			if(inputWires[i].GetComponent<WireGroupScript>().isPowered()){
				return true;
			}
		}
		return poweredProperly;
	}
	// Update is called once per frame
	void Update () {

		if(typeOfGate == GateType.AND){
			isLettingPowerThrough = andGateCheck ();
		}
		else if(typeOfGate == GateType.XOR){
			isLettingPowerThrough = xorGateCheck ();
		}
		else if(typeOfGate == GateType.NAND){
			isLettingPowerThrough = nandGateCheck ();
		}
		else if(typeOfGate == GateType.OR){
			isLettingPowerThrough = orGateCheck ();
		}

		if (isLettingPowerThrough && outputWire.GetComponent<WireGroupScript> ().isPowered () == false) {
			outputWire.GetComponent<WireGroupScript> ().turnOn ();
			Debug.Log("Set Gate output to true");
		}
		else if(isLettingPowerThrough == false && outputWire.GetComponent<WireGroupScript> ().isPowered () ){
			outputWire.GetComponent<WireGroupScript> ().turnOff ();
			Debug.Log("Set Gate output to false");
		}
	}	
}
