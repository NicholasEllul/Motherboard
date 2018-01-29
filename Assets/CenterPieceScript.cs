using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPieceScript : MonoBehaviour {

	public WireGroupScript[] powerSources;
	private SpriteRenderer centerSprite;
	private int prevPowerSourceCounter;
	public void updatePowerSource(){
		int powerSourceCounter = 0;
		for (int i = 0; i < powerSources.Length; i++) {
			if (powerSources [i].on == true) {
				powerSourceCounter++;
			}
		}
		centerSprite.color = new Color (1 - (1f / powerSources.Length * powerSourceCounter), centerSprite.color.g, centerSprite.color.b);

		if (powerSourceCounter > prevPowerSourceCounter) {
			GetComponentInChildren<ParticleSystem> ().Emit (20);
		}
		prevPowerSourceCounter = powerSourceCounter;
	}
		
	// Use this for initialization
	void Start () {
		centerSprite = GetComponent<SpriteRenderer> ();
		prevPowerSourceCounter = 0;
		updatePowerSource ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
