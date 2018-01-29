using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFade : MonoBehaviour {
    public static bool startFading = false;
    public SpriteRenderer colour;

	// Use this for initialization
	void Start () {
        colour = GetComponent<SpriteRenderer>();
        Debug.Log(colour.color.a);

    }
	
   
	// Update is called once per frame
	void Update () {
        if (startFading == true)
        {
            Color tempColour = colour.color;
            tempColour.a -= .03f;
            colour.color = tempColour;
            Debug.Log(colour.color.a);
        }
		
	}
}
