using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour {

	// StealthPlayerSwitcher thisPlayer;
	PlayerIdentifier thisPlayer;
	private Camera myCam;
	void Start () {
		// thisPlayer = GetComponentInParent<StealthPlayerSwitcher>();
		thisPlayer = GetComponentInParent<PlayerIdentifier>();
		myCam = GetComponent<Camera>();	
		if (thisPlayer.myPlayerNum == 0){
			myCam.targetDisplay = 0;
		} else if(thisPlayer.myPlayerNum == 1) {
			myCam.targetDisplay = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
