using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerTimeManager : MonoBehaviour {

	private PlayerIdentifier playerIdentifier;
	// public GameObject myCanvas; 
	public float ap_jumpCost = 20f;
	public float ap_walkCost = 1f;

	public float ap_lookCost = 0.2f;
	public float ap_attackCost = 50f;
	float mouseX_pos;
	float mouseY_pos;

 	public GameObject otherPlayer;
	public GameObject myCanvas;
	private StealthPlayerSwitcher playerSwitcher;
	private PlayerTimeManager timeManager;

	private FirstPersonController firstPersonController;
	bool isFrozen;
	Rigidbody rb;

	public enum PlayerFrozenState{
		Frozen,
		Not_Frozen
	}
	public PlayerFrozenState playerFrozenState;
	public KeyCode switchKey;

	void Start () {
		playerIdentifier = GetComponent<PlayerIdentifier>();
		firstPersonController = GetComponent<FirstPersonController>();
		playerFrozenState = PlayerFrozenState.Frozen;
		rb = GetComponent<Rigidbody>();
 		playerSwitcher = GetComponent<StealthPlayerSwitcher>();
		
		//pick a certain canvas depending on playernumber.
		// if(GameObject.FindGameObjectWithTag("Player") != this.gameObject){
		// 	otherPlayer = GameObject.FindGameObjectWithTag("Player");
		// }

		StartCoroutine(InitOtherPlayer(0.2f));
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
				//if player is out of time,
				TrackPlayerAction();
				SwitchToOtherPlayerTemp(switchKey);
			}
		}			
	}

	public void TrackPlayerAction(){
		//restrict jumping based on AP
		if(TimeManager.actionPoints < ap_jumpCost){
			firstPersonController.canJump = false;
		} else {
			firstPersonController.canJump = true;
		}

		//Deplete AP when you move
		if(CrossPlatformInputManager.GetAxis("Horizontal") != 0 || CrossPlatformInputManager.GetAxis("Vertical") != 0){
			TimeManager.DepleteAP(ap_walkCost);
 		}

		if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0){
			TimeManager.DepleteAP(ap_lookCost);
		}

		//deplete AP when you jump and update alert text
		if(CrossPlatformInputManager.GetButtonDown("Jump")){
			// float ap_cost = 20f;
 			if(TimeManager.actionPoints >= ap_jumpCost){
				TimeManager.DepleteAP(ap_jumpCost);
			} else {
				TimeManager.apAlertString = "Not enough AP to jump!";
				Invoke("ClearAlertString", 3f);
			}
		}

		//deplete AP when you fire
		if(CrossPlatformInputManager.GetButtonDown("Fire1")){
			// Debug.Log("Lol! You fired!");
			// otherPlayer.GetComponent<PlayerHealthManager>().DepleteHealth(10);
			if(TimeManager.actionPoints >= ap_attackCost){
				TimeManager.DepleteAP(ap_attackCost);
			} else {
				TimeManager.apAlertString = "Not enough AP to fire!";
				Invoke("ClearAlertString", 3f);
			}
		}

		if(TimeManager.actionPoints <= 0){
			FreezeMe();
			myCanvas.GetComponent<Canvas>().enabled = true;
			Invoke("SwitchToOtherPlayer", 3.5f);
			// SwitchToOtherPlayer();
			return;
		}
		
	}

	public void FreezeMe(){
		// Debug.Log("Freezing me!");
		playerFrozenState = PlayerFrozenState.Frozen;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.useGravity = false;
		GetComponent<CharacterController>().enabled = false;
		GetComponent<FirstPersonController>().enabled = false;
		// Debug.Log(playerFrozenState);
	}

	public void UnFreezeMe(){
		Debug.Log("Unfreezing me!");
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
			myCanvas.GetComponent<Canvas>().enabled = true;
			//load a UI screen that says "Switching to Other Player". could have a bit of delay.
			// StartCoroutine(ActivatePlayerSwitchCanvas(0.01f));
			Invoke("SwitchToOtherPlayer", 5f);
		} 
	}

	// void SwitchToOtherPlayer(){
	// 	Debug.Log("Switching to other player without input!");
	// 	//freeze or unfreeze this player; this should happen immediately.
	// 	FreezeMe();
	// 	canvas.GetComponent<Canvas>().enabled = true;
	// 	//load a UI screen that says "Switching to Other Player". could have a bit of delay.
	// 	// StartCoroutine(ActivatePlayerSwitchCanvas(0.01f));
	// 	Invoke("SwitchToOtherPlayer", 3.5f);
 	// }

	public void SwitchToOtherPlayer(){
        // yield return new WaitForSeconds(delay);
        //talk to the StealthPlayerSwitcher script on the other player.
		Debug.Log("Switching to other player without input!");
		TimeManager.ResetAP();
        myCanvas.GetComponent<Canvas>().enabled = false;
        GetComponentInChildren<Camera>().enabled = false;
		GetComponentInChildren<AudioListener>().enabled = false;
        CurrentPlayerTracker.SetCurrentPlayer(otherPlayer);
		playerSwitcher.GetComponent<StealthPlayerSwitcher>().otherPlayer.GetComponent<StealthPlayerSwitcher>().SwitchToThis();
		playerSwitcher.GetComponent<StealthPlayerSwitcher>().otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<Canvas>().enabled = true;;
    }


	IEnumerator InitOtherPlayer(float delay){
		yield return new WaitForSeconds(delay);
		transform.eulerAngles = new Vector3 (0, 76, 0);
		otherPlayer = playerSwitcher.otherPlayer;
		if(playerSwitcher.myIndex == 1){
			GetComponentInChildren<AudioListener>().enabled = false;
 		}
	}

	IEnumerator InitCanvas(float delay){
		yield return new WaitForSeconds(delay);
		myCanvas.GetComponent<Canvas>().enabled = false;
	}

	void ClearAlertString(){
		TimeManager.ClearAlertString();
	}

	

	
}
