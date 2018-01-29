using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public Scene sceneToSwitchTo;
    public static bool SceneCheck;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public IEnumerator Delay(float time, string scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
	public void PlayGame(string scene){
        ObjectFade.startFading = true;
        StartCoroutine(Delay(1,scene));


	}

}
