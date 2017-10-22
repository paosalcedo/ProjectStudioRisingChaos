using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerTimeManager : MonoBehaviour {

	private GameObject otherPlayer;
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

	void Start () {
		playerFrozenState = PlayerFrozenState.Not_Frozen;
		rb = GetComponent<Rigidbody>();
 		playerSwitcher = GetComponent<StealthPlayerSwitcher>();
		canvas = GameObject.FindGameObjectWithTag("Canvas");
		if(GameObject.FindGameObjectWithTag("Player") != this){
			otherPlayer = GameObject.FindGameObjectWithTag("Player");
		}
		canvas.GetComponent<Canvas>().enabled = false;
	
	}

	// Update is called once per frame
	void Update () {
		Debug.Log("player " + playerSwitcher.myIndex + " is " + playerFrozenState);
		if(playerFrozenState == PlayerFrozenState.Frozen){
			FreezeMe();
		}
		else if (playerFrozenState == PlayerFrozenState.Not_Frozen){
			UnFreezeMe();
		}

		SwitchToOtherPlayerTemp();
		
	}

	void FreezeMe(){
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.useGravity = false;
		GetComponent<CharacterController>().enabled = false;
		GetComponent<FirstPersonController>().enabled = false;
		// Debug.Log(playerFrozenState);
	}

	void UnFreezeMe(){
		rb.constraints = RigidbodyConstraints.None;
		rb.useGravity = true;
		GetComponent<CharacterController>().enabled = true;
		GetComponent<FirstPersonController>().enabled = true;
		// Debug.Log(playerFrozenState);
	}

	//if time is <= 0, freeze this player, load UI screen that says "Switching to Player 2", then complete the switch.
	
	void SwitchToOtherPlayerTemp(){
		if(Input.GetKeyDown(KeyCode.Return)){
			//freeze or unfreeze this player; this should happen immediately.
			if(playerFrozenState == PlayerFrozenState.Not_Frozen){
				playerFrozenState = PlayerFrozenState.Frozen;
			} else if (playerFrozenState == PlayerFrozenState.Frozen){
				playerFrozenState = PlayerFrozenState.Not_Frozen;
			}

			//load a UI screen that says "Switching to Other Player". could have a bit of delay.
			StartCoroutine(ActivatePlayerSwitchCanvas(5f));
		} 
	}

	IEnumerator ActivatePlayerSwitchCanvas(float delay){
		yield return new WaitForSeconds(delay);
		canvas.GetComponent<Canvas>().enabled = true;
		// StartCoroutine(SwitchToOtherPlayer(5f));
	}

	IEnumerator SwitchToOtherPlayer(float delay){
		yield return new WaitForSeconds(delay);
		//talk to the StealthPlayerSwitcher script on the other player.
		canvas.SetActive(false);
		otherPlayer.GetComponent<StealthPlayerSwitcher>().SwitchToThis();
		// playerSwitcher.
	}
	

	
}
