using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour {

	StealthPlayerSwitcher thisPlayer;
	private Camera myCam;
	void Start () {
		thisPlayer = GetComponentInParent<StealthPlayerSwitcher>();
		myCam = GetComponent<Camera>();	
		if (thisPlayer.myIndex == 0){
			myCam.targetDisplay = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
