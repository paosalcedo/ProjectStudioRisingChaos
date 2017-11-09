using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StealthPlayerSwitcher : MonoBehaviour {

	private PlayerTimeManager timeManager;
	private PlayerTimeManager otherTimeManager;

	private PlayerIdentifier playerIdentifier;
	public GameObject currentPlayer;
	public GameObject otherPlayer;

	private Vector3 startPos;

	public int myIndex;
	public int otherIndex;
	// public int myPlayerIndex;

	// Use this for initialization

	void Awake(){
		playerIdentifier = GetComponent<PlayerIdentifier>();
		timeManager = GetComponent<PlayerTimeManager>();
		// otherPlayer = GameObject.FindGameObjectWithTag("Player");
	}
	void Start () {
		// myIndex = Random.Range(0, 2);
		// otherTimeManager = otherPlayer.GetComponent<PlayerTimeManager>();
		// otherIndex = otherPlayer.GetComponent<StealthPlayerSwitcher>().myIndex;
		//add if statement to check if this player is player 0
		if(playerIdentifier.myPlayerNum == 0){
			// CurrentPlayerTracker.SetCurrentPlayer(this.gameObject);
			CurrentPlayerTracker.currentPlayer = this.gameObject;
			// Debug.Log("Active player is " + CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myPlayerNum);
			// timeManager.UnFreezeMe();
			// Debug.Log("Current active player name is " + CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myName);
 			// Debug.Log("assigning current player to PLayer 0");
		} else if (playerIdentifier.myPlayerNum == 1){
			CurrentPlayerTracker.otherPlayer = this.gameObject;
			// Debug.Log("Other player is " + CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myPlayerNum);
			// timeManager.FreezeMe();
		} 
		
		// for (int i = 0; i < 2; i++){
		// 	myIndex = i;
		// 	if(myIndex == otherIndex){
		// 		//set player 0 to current player.
		// 		myIndex = i-1;
		// 		if(myIndex == 0){
		// 			// CurrentPlayerTracker.SetCurrentPlayer(this.gameObject);
		// 			// currentPlayer = CurrentPlayerTracker.currentPlayer;
		// 		} 
		// 		//Freeze the other player; disable their camera
		// 		// otherTimeManager.FreezeMe();
		// 		// otherTimeManager.GetComponentInChildren<Camera>().enabled = false;		
		// 	}
		// }
		startPos = transform.position;
 		// StartCoroutine(InitOtherPlayer(0.1f));
	}
	
	// Update is called once per frame
	void Update(){
 	}
	public void SwitchToThis(){
		timeManager.UnFreezeMe();
		timeManager.myActionPoints = timeManager.maxActionPoints;
		// CurrentPlayerTracker.currentPlayer = this.gameObject;
		// Debug.Log("SwitchToThis() was called");
 		// GetComponentInChildren<Camera>().enabled = true;
		// GetComponentInChildren<AudioListener>().enabled = true;
	}

	IEnumerator InitOtherPlayer(float delay){
		yield return new WaitForSeconds(delay);
		if(myIndex == 1){
 			otherPlayer = CurrentPlayerTracker.currentPlayer;
			timeManager.switchKey = KeyCode.RightBracket;
		}
	}
}
