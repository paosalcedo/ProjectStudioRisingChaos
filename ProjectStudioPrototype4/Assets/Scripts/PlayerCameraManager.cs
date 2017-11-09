using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour {

	// StealthPlayerSwitcher thisPlayer;
	public GameObject myCanvas;
	PlayerIdentifier thisPlayerID;
	GameObject thisPlayer;
	private Camera myCam;
	public int myDisplayNum;
	void Start () {
		thisPlayerID = GetComponentInParent<PlayerIdentifier>();
		thisPlayer = transform.parent.gameObject;
		myCam = GetComponent<Camera>();	
		if (thisPlayerID.myPlayerNum == 0){
			myCam.targetDisplay = 0;
			myCanvas.layer = 8;
			myCam.cullingMask = ~(1 << LayerMask.NameToLayer("P2_UI"));
			myDisplayNum = myCam.targetDisplay;
		} else if(thisPlayerID.myPlayerNum == 1) {
			myCam.targetDisplay = 1;
			myCanvas.layer = 9;
			myCam.cullingMask = ~(1 << LayerMask.NameToLayer("P1_UI"));
			myDisplayNum = myCam.targetDisplay;
		}
	}
	
	// Update is called once per frame
 
	public void ChangeCullingMask(){
		// Debug.Log("Culling mask changed!");
		myCam.cullingMask = (1 << LayerMask.NameToLayer("P1_UI"));
		Debug.Log(LayerMask.NameToLayer("P1_UI"));
		
	}
}
