﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerTimeManager : MonoBehaviour {

	public KeyCode endTurnKey;
	private PlayerIdentifier playerIdentifier;
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

	private PlayerCanvasUpdater myCanvasUpdater;
	private FirstPersonController firstPersonController;
	bool isFrozen;
	Rigidbody rb;

	public enum PlayerFrozenState{
		Frozen,
		Not_Frozen
	}
	public PlayerFrozenState playerFrozenState;
	public KeyCode switchKey;

	public List<Pickup> pickups = new List<Pickup>();

	void Start () {
		pickups.AddRange(FindObjectsOfType<Pickup>());
		// otherPlayer = GetComponent<StealthPlayerSwitcher>().otherPlayer;
		maxActionPoints = myActionPoints;
		playerIdentifier = GetComponent<PlayerIdentifier>();
		myCanvasUpdater = myCanvas.GetComponent<PlayerCanvasUpdater>();
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
			//Turn on audiolistener?
			FreezeMe();
			if(playerIdentifier.myPlayerNum == 0){
				myCanvasUpdater.turnText.text = CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName + "'s turn";

			} else {
				myCanvasUpdater.turnText.text = CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myName + "'s turn";
			}
			break;

			case PlayerFrozenState.Not_Frozen:
			EndTurn(endTurnKey);
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
		// if(TimeManager.actionPoints < ap_jumpCost){
		// 	// firstPersonController.Get
		// 	firstPersonController.canJump = false;
		// } else {
		// 	firstPersonController.canJump = true;
 		// }

		//Deplete AP when you move
		if(playerIdentifier.myPlayerNum	== 0){
			if(CrossPlatformInputManager.GetAxisRaw("Horizontal") != 0 || CrossPlatformInputManager.GetAxisRaw("Vertical") != 0){
				// TimeManager.DepleteAP(ap_walkCost);
				if(!firstPersonController.m_Crouching){
					myActionPoints -= ap_walkCost;
				} else if (firstPersonController.m_Crouching){
					myActionPoints -= ap_walkCost*(firstPersonController.m_WalkSpeed/firstPersonController.m_RunSpeed);
				}
			}
		}

		if(playerIdentifier.myPlayerNum	== 1){
			if(CrossPlatformInputManager.GetAxisRaw("P2_Horizontal") != 0 || CrossPlatformInputManager.GetAxisRaw("P2_Vertical") != 0){
				// TimeManager.DepleteAP(ap_walkCost);
				if(!firstPersonController.m_Crouching){
					myActionPoints -= ap_walkCost;
				} else if (firstPersonController.m_Crouching){
					myActionPoints -= ap_walkCost*(firstPersonController.m_WalkSpeed/firstPersonController.m_RunSpeed);
				}
			}
		}

		if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0){
			// TimeManager.DepleteAP(ap_lookCost);
			myActionPoints -= ap_lookCost;
		}

		//deplete AP when you jump and update alert text
		if(GetComponent<JumpScript>().isJumping){
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
			turnEnded = true;
			// FreezeMe();
 			myCanvas.GetComponent<Canvas>().enabled = true;
			Invoke("SwitchToOtherPlayer", 3.5f);
			// SwitchToOtherPlayer();
			return;
		}		
	}

	public bool turnCounted = false;
	public void FreezeMe(){
		// Debug.Log("Freezing player " + playerIdentifier.m`yPlayerNum);
		// playerFrozenState = PlayerFrozenState.Frozen;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.useGravity = false;
		if(!turnCounted){
			foreach (Pickup pickup in pickups){
				if(pickup.hasBeenPickedUp){
					pickup.IncrementTurnCounter();
				}
			}
			turnCounted = true;
		} 
		GetComponent<CharacterController>().enabled = false;
		GetComponent<FirstPersonController>().enabled = false;
		// Debug.Log(playerFrozenState);
	}

	public bool turnEnded;

	public void UnFreezeMe(){
		// Debug.Log("Unfreezing player " + playerIdentifier.myPlayerNum);
		isActive = true;
		turnCounted = false;
		playerFrozenState = PlayerFrozenState.Not_Frozen;
		rb.constraints = RigidbodyConstraints.None;
		rb.useGravity = true;						
		myCanvasUpdater.turnText.text = "";
		GetComponentInChildren<WeaponSwitcher>().ResetWeaponCooldowns();
		GetComponent<CharacterController>().enabled = true;
		GetComponent<FirstPersonController>().enabled = true;
		// Debug.Log(playerFrozenState);
	}

	private void EndTurn(KeyCode key){
		if(Input.GetKeyDown(key))
			myActionPoints = 0;
	}

	public void SwitchToOtherPlayer(){
		//only run this code if you're not frozen, aka you're the active player.
		if(isActive){
			if(playerIdentifier.myPlayerNum == 0){
				CurrentPlayerTracker.otherPlayer.GetComponent<StealthPlayerSwitcher>().SwitchToThis();
				GetComponentInChildren<CameragunControl>().ResetCameraCount();
				isActive = false;
			}
			if(playerIdentifier.myPlayerNum == 1){
				CurrentPlayerTracker.currentPlayer.GetComponent<StealthPlayerSwitcher>().SwitchToThis();
				GetComponentInChildren<CameragunControl>().ResetCameraCount();
				isActive = false;
 			}
		}	
    }

	public void PickupActionPoints(int apPickedUp){
		PlayerCanvasUpdater myCanvasUpdater = myCanvas.GetComponent<PlayerCanvasUpdater>();
		myCanvasUpdater.hitAlertText.text = "Gained " + apPickedUp + " Action Points!";
		StartCoroutine(myCanvasUpdater.ClearText(2f, myCanvasUpdater.hitAlertText));
	}
	
}
