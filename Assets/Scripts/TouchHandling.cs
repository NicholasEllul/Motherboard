using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchHandling : MonoBehaviour {

	public static Camera cam;

	private Touch[] touches;
	private float previousTouchTime;
	public AudioClip spiralSound;
	public void getCamera(){
		cam = Camera.main;
	}

	void Start(){
		getCamera ();
	}
	void Awake () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {

			touches = Input.touches;
			if (Time.time - previousTouchTime > 0.5f) {

				for (int i = 0; i < touches.Length; i++) {

					Vector3 touchWorldPoint = cam.ScreenToWorldPoint (touches [i].position);

					if (touches [i].phase == TouchPhase.Began) {
						previousTouchTime = Time.time;
						Debug.Log ("Began");
						RaycastHit2D hit = Physics2D.Raycast (touchWorldPoint, cam.transform.forward);
						string tag = hit.collider.tag;
						if (tag == "Switch") {
							hit.transform.GetComponent<SwitchScript> ().hit ();
						} else if (tag == "Spiral") {
							hit.transform.GetComponent<SpiralWheelSciript> ().hit ();
						}
					} 
				}
			}
		}
	}
}
