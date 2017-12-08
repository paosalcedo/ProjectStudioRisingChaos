using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("displays connected: " + Display.displays.Length);
		if(Display.displays.Length > 1){
			Display.displays[1].Activate(1920, 1080, 60);
			Debug.Log("Activated second display!");
		}
	}

}
