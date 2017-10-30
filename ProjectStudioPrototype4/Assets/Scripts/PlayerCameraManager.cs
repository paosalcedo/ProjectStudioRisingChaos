using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour {

	// StealthPlayerSwitcher thisPlayer;
	PlayerIdentifier thisPlayer;
	private Camera myCam;
	public int myDisplayNum;
	void Start () {
		// thisPlayer = GetComponentInParent<StealthPlayerSwitcher>();
		thisPlayer = GetComponentInParent<PlayerIdentifier>();
		myCam = GetComponent<Camera>();	
		if (thisPlayer.myPlayerNum == 0){
			myCam.targetDisplay = 0;
			myDisplayNum = myCam.targetDisplay;
		} else if(thisPlayer.myPlayerNum == 1) {
			myCam.targetDisplay = 1;
			myDisplayNum = myCam.targetDisplay;
		}
	}
	
	// Update is called once per frame
 
}
