using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StealthPlayerSwitcher : MonoBehaviour {

	private PlayerTimeManager timeManager;
	private PlayerTimeManager otherTimeManager;

	public GameObject currentPlayer;
	public GameObject otherPlayer;

	private Vector3 startPos;

	public int myIndex;
	public int otherIndex;
	// public int myPlayerIndex;

	// Use this for initialization

	void Awake(){
		timeManager = GetComponent<PlayerTimeManager>();
		otherPlayer = GameObject.FindGameObjectWithTag("Player");
	}
	void Start () {
		// myIndex = Random.Range(0, 2);
		otherTimeManager = otherPlayer.GetComponent<PlayerTimeManager>();
		otherIndex = otherPlayer.GetComponent<StealthPlayerSwitcher>().myIndex;
		for (int i = 0; i < 2; i++){
			myIndex = i;
			if(myIndex == otherIndex){
				//set player 0 to current player.
				myIndex = i-1;
				if(myIndex == 0){
					CurrentPlayerTracker.SetCurrentPlayer(this.gameObject);
					currentPlayer = CurrentPlayerTracker.currentPlayer;
				} 
				//Freeze the other player; disable their camera
				otherTimeManager.FreezeMe();
				otherTimeManager.GetComponentInChildren<Camera>().enabled = false;		
			}
		}
		startPos = transform.position;
		// Debug.Log("My index is " + myIndex);
		StartCoroutine(InitOtherPlayer(0.1f));
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SwitchToThis(){
		timeManager.UnFreezeMe();
 		GetComponentInChildren<Camera>().enabled = true;
		GetComponentInChildren<AudioListener>().enabled = true;
	}

	IEnumerator InitOtherPlayer(float delay){
		yield return new WaitForSeconds(delay);
		if(myIndex == 1){
 			otherPlayer = CurrentPlayerTracker.currentPlayer;
			timeManager.switchKey = KeyCode.RightBracket;
		}
	}
}
