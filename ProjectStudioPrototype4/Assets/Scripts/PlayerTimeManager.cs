using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerTimeManager : MonoBehaviour {

	bool isFrozen;
	Rigidbody rb;

	public enum PlayerFrozenState{
		Frozen,
		Not_Frozen
	}
	public PlayerFrozenState playerFrozenState;

	void Start () {
		rb = GetComponent<Rigidbody>();
		isFrozen = false;
	}

	// Update is called once per frame
	void Update () {
		if(playerFrozenState == PlayerFrozenState.Frozen){
			FreezeMe();
		}
		else if (playerFrozenState == PlayerFrozenState.Not_Frozen){
			UnFreezeMe();
		}

		if(Input.GetKeyDown(KeyCode.Return)){
			if(playerFrozenState == PlayerFrozenState.Not_Frozen){
				playerFrozenState = PlayerFrozenState.Frozen;
			} else if (playerFrozenState == PlayerFrozenState.Frozen){
				playerFrozenState = PlayerFrozenState.Not_Frozen;
			}			
		} 
	}

	void FreezeMe(){
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.useGravity = false;
		GetComponent<CharacterController>().enabled = false;
		GetComponent<FirstPersonController>().enabled = false;
		Debug.Log(playerFrozenState);
	}

	void UnFreezeMe(){
		rb.constraints = RigidbodyConstraints.None;
		rb.useGravity = true;
		GetComponent<CharacterController>().enabled = true;
		GetComponent<FirstPersonController>().enabled = true;
		Debug.Log(playerFrozenState);
	}

	
}
