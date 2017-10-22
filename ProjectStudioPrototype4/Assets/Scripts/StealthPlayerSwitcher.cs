using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthPlayerSwitcher : MonoBehaviour {

	private PlayerTimeManager timeManager;
	public GameObject otherPlayer;
	private Vector3 startPos;

	public int myIndex;
	// public int myPlayerIndex;

	// Use this for initialization
	void Start () {
		if(GameObject.FindGameObjectWithTag("Player") != this){
			otherPlayer = GameObject.FindGameObjectWithTag("Player");
			myIndex = Random.Range(0,2);
			if(myIndex == otherPlayer.GetComponent<StealthPlayerSwitcher>().myIndex){
				myIndex = Random.Range(0,2);
				if(myIndex == 0)
					CurrentPlayerTracker.SetCurrentPlayer(this.gameObject);
				else if(myIndex != 0)
					this.gameObject.SetActive(false);			
			}
		}
		startPos = transform.position;
		timeManager = GetComponent<PlayerTimeManager>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Current player is player " + CurrentPlayerTracker.currentPlayer.GetComponent<StealthPlayerSwitcher>().myIndex);
	}

	public void SwitchToThis(){
 		timeManager.playerFrozenState = PlayerTimeManager.PlayerFrozenState.Not_Frozen;
		// GetComponentInChildren<Camera>().enabled = false;
		// GetComponent<CharacterController>().enabled = false;
		// GetComponent<FirstPersonController>().enabled = false;
		// GetComponent<AudioSource>().enabled = false;
	}
}
