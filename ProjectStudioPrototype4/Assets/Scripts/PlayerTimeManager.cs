using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerTimeManager : MonoBehaviour {

	public GameObject otherPlayer;
	private GameObject canvas;
	private StealthPlayerSwitcher playerSwitcher;
	private PlayerTimeManager timeManager;
	bool isFrozen;
	Rigidbody rb;

	public enum PlayerFrozenState{
		Frozen,
		Not_Frozen
	}
	public PlayerFrozenState playerFrozenState;
	public KeyCode switchKey;

	void Start () {
		playerFrozenState = PlayerFrozenState.Not_Frozen;
		rb = GetComponent<Rigidbody>();
 		playerSwitcher = GetComponent<StealthPlayerSwitcher>();
		canvas = GameObject.FindGameObjectWithTag("Canvas");
		// if(GameObject.FindGameObjectWithTag("Player") != this.gameObject){
		// 	otherPlayer = GameObject.FindGameObjectWithTag("Player");
		// }

		StartCoroutine(InitOtherPlayer(0.2f));
		canvas.GetComponent<Canvas>().enabled = false;
	}

	// Update is called once per frame
	void Update () {
		// Debug.Log("player " + playerSwitcher.myIndex + " is " + playerFrozenState);
		if(playerFrozenState == PlayerFrozenState.Frozen){
			FreezeMe();
		}
		else if (playerFrozenState == PlayerFrozenState.Not_Frozen){
			UnFreezeMe();
			if(CurrentPlayerTracker.currentPlayer == this.gameObject){
				SwitchToOtherPlayerTemp(switchKey);
			}
		}

				
	}

	public void FreezeMe(){
		playerFrozenState = PlayerFrozenState.Frozen;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.useGravity = false;
		GetComponent<CharacterController>().enabled = false;
		GetComponent<FirstPersonController>().enabled = false;
		// Debug.Log(playerFrozenState);
	}

	public void UnFreezeMe(){
		playerFrozenState = PlayerFrozenState.Not_Frozen;
		rb.constraints = RigidbodyConstraints.None;
		rb.useGravity = true;
		GetComponent<CharacterController>().enabled = true;
		GetComponent<FirstPersonController>().enabled = true;
		// Debug.Log(playerFrozenState);
	}

	//if time is <= 0, freeze this player, load UI screen that says "Switching to Player 2", then complete the switch.
	
	void SwitchToOtherPlayerTemp(KeyCode key){
		if(Input.GetKeyDown(key)){
			//freeze or unfreeze this player; this should happen immediately.
			FreezeMe();
			canvas.GetComponent<Canvas>().enabled = true;
			//load a UI screen that says "Switching to Other Player". could have a bit of delay.
			// StartCoroutine(ActivatePlayerSwitchCanvas(0.01f));
			Invoke("SwitchToOtherPlayer", 5f);
		} 
	}

	

	public void SwitchToOtherPlayer(){
		// yield return new WaitForSeconds(delay);
		//talk to the StealthPlayerSwitcher script on the other player.
		canvas.GetComponent<Canvas>().enabled = false;
		GetComponentInChildren<Camera>().enabled = false;
		CurrentPlayerTracker.SetCurrentPlayer(otherPlayer);
		playerSwitcher.GetComponent<StealthPlayerSwitcher>().otherPlayer.GetComponent<StealthPlayerSwitcher>().SwitchToThis();
		// playerSwitcher.
	}

	IEnumerator InitOtherPlayer(float delay){
		yield return new WaitForSeconds(delay);
		otherPlayer = playerSwitcher.otherPlayer;
		if(playerSwitcher.myIndex == 1){
			GetComponentInChildren<AudioListener>().enabled = false;
		}
	}
	

	
}
