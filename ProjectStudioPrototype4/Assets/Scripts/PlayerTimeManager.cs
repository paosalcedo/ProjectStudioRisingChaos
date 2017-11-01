using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerTimeManager : MonoBehaviour {

	private PlayerIdentifier playerIdentifier;
	// public GameObject myCanvas; 
	public bool isActive;
	public float ap_jumpCost = 20f;
	public float ap_walkCost = 1f;

	public float ap_lookCost = 0.2f;
	public float ap_attackCost = 50f;

	public float myActionPoints;
	public float maxActionPoints;
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
		// otherPlayer = GetComponent<StealthPlayerSwitcher>().otherPlayer;
		maxActionPoints = myActionPoints;
		playerIdentifier = GetComponent<PlayerIdentifier>();
		firstPersonController = GetComponent<FirstPersonController>();
		playerFrozenState = PlayerFrozenState.Frozen;
		rb = GetComponent<Rigidbody>();
 		playerSwitcher = GetComponent<StealthPlayerSwitcher>();
		if(playerIdentifier.myPlayerNum == 0){
			UnFreezeMe();
			// Debug.Log("I'm unfrozen! I am player " + playerIdentifier.myPlayerNum);
		} 
		if(playerIdentifier.myPlayerNum == 1){
			FreezeMe();
			// Debug.Log("I'm frozen! I am player " + playerIdentifier.myPlayerNum);
		}
	}

	// Update is called once per frame
	void Update () {
		// Debug.Log(playerFrozenState + " " + playerIdentifier.myPlayerNum);
		// Debug.Log("player " + playerSwitcher.myIndex + " is " + playerFrozenState);
		// Debug.Log("Active player is " + CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myName);
		// Debug.Log("Inactive player is " + CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName);

		switch (playerFrozenState){
			case PlayerFrozenState.Frozen:
			FreezeMe();

			break;

			case PlayerFrozenState.Not_Frozen:
			UnFreezeMe();
			UpdateCanvasAP(myActionPoints);
				//if player is out of time,
			TrackPlayerAction();
				// SwitchToOtherPlayerTemp(switchKey);
			// }
			break;

			default:
			break;

		}
		// if(playerFrozenState == PlayerFrozenState.Frozen){
		// 	FreezeMe();
		// 	// Debug.Log(playerIdentifier.myName + " is being frozen in PlayerTimeManager");

		// }
		// if (playerFrozenState == PlayerFrozenState.Not_Frozen){
		// 	UnFreezeMe();
		// 	// Debug.Log(playerIdentifier.myName + " is being unfrozen in PlayerTimeManager");
		// 	// if(CurrentPlayerTracker.currentPlayer == this.gameObject){
		// 		//if player is out of time,
		// 		TrackPlayerAction();
		// 		// SwitchToOtherPlayerTemp(switchKey);
		// 	// }
		// }			
	}

	public void UpdateCanvasAP(float myActionPoints){
		if(myActionPoints >= 0){
			Text apText = myCanvas.GetComponent<PlayerCanvasUpdater>().apText;
			apText.text = "AP: " + myActionPoints.ToString("F0") + "/" + maxActionPoints;
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
			// TimeManager.DepleteAP(ap_walkCost);
			myActionPoints -= ap_walkCost;
 		}

		if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0){
			// TimeManager.DepleteAP(ap_lookCost);
			myActionPoints -= ap_lookCost;
		}

		//deplete AP when you jump and update alert text
		if(CrossPlatformInputManager.GetButtonDown("Jump")){
			// float ap_cost = 20f;
 			// if(TimeManager.actionPoints >= ap_jumpCost){
			if(myActionPoints >= ap_jumpCost){
				// TimeManager.DepleteAP(ap_jumpCost);
				myActionPoints -= ap_jumpCost;
			} else {
				TimeManager.apAlertString = "Not enough AP to jump!";
				Invoke("ClearAlertString", 3f);
			}
		}

		//deplete AP when you fire
		if(CrossPlatformInputManager.GetButtonDown("Fire1")){
			// Debug.Log("Lol! You fired!");
			// otherPlayer.GetComponent<PlayerHealthManager>().DepleteHealth(10);
			if(myActionPoints >= ap_attackCost){
			// if(TimeManager.actionPoints >= ap_attackCost){
				// TimeManager.DepleteAP(ap_attackCost);
			} 
		}

		if(myActionPoints <= 0){
		// if(TimeManager.actionPoints <= 0){
			playerFrozenState = PlayerFrozenState.Frozen;
			// FreezeMe();
 			myCanvas.GetComponent<Canvas>().enabled = true;
			Invoke("SwitchToOtherPlayer", 3.5f);
			// SwitchToOtherPlayer();
			return;
		}		
	}

	public void FreezeMe(){
		// Debug.Log("Freezing player " + playerIdentifier.myPlayerNum);
		// playerFrozenState = PlayerFrozenState.Frozen;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.useGravity = false;
		GetComponent<CharacterController>().enabled = false;
		GetComponent<FirstPersonController>().enabled = false;
		// Debug.Log(playerFrozenState);
	}

	public void UnFreezeMe(){
		// Debug.Log("Unfreezing player " + playerIdentifier.myPlayerNum);
		isActive = true;
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
		//only run this code if you're not frozen, aka you're the active player.
		if(isActive){
			if(playerIdentifier.myPlayerNum == 0){
				CurrentPlayerTracker.otherPlayer.GetComponent<StealthPlayerSwitcher>().SwitchToThis();
				isActive = false;
			}
			if(playerIdentifier.myPlayerNum == 1){
				CurrentPlayerTracker.currentPlayer.GetComponent<StealthPlayerSwitcher>().SwitchToThis();
				isActive = false;
				// .GetComponent<StealthPlayerSwitcher>().SwitchToThis();
			}
		}
		// GetComponentInChildren<AudioListener>().enabled = false;


        // yield return new WaitForSeconds(delay);
        //talk to the StealthPlayerSwitcher script on the other player.
		// Debug.Log("Switching to other player without input!");
		// myActionPoints = 100f;
        // myCanvas.GetComponent<Canvas>().enabled = false;
        // GetComponentInChildren<Camera>().enabled = false;
        // CurrentPlayerTracker.SetCurrentPlayer(otherPlayer);
		// Debug.Log("Current player is now " + CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myName);
		// CurrentPlayerTracker.currentPlayer = CurrentPlayerTracker.otherPlayer;
		// CurrentPlayerTracker.otherPlayer = this.gameObject;
		// CurrentPlayerTracker.otherPlayer.GetComponent<StealthPlayerSwitcher>().SwitchToThis();
		// CurrentPlayerTracker.otherPlayer = this.gameObject;
		// CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<Canvas>().enabled = true;
		// playerSwitcher.GetComponent<StealthPlayerSwitcher>().otherPlayer.GetComponent<StealthPlayerSwitcher>().SwitchToThis();
		// playerSwitcher.GetComponent<StealthPlayerSwitcher>().otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<Canvas>().enabled = true;
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
