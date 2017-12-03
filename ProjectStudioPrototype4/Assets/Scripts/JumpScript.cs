using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class JumpScript : MonoBehaviour {

	public PlayerTimeManager thisPlayerTimeManager;

	public FirstPersonController fpc;
	public KeyCode jumpKey;
	public KeyCode P2_jumpKey;
	public bool isJumping;
	private float myAPcost;
	// Use this for initialization
	void Start () {
		myAPcost = thisPlayerTimeManager.ap_jumpCost;
	}
	
	// Update is called once per frame
	void Update () {
		if(thisPlayerTimeManager.playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
			if(thisPlayerTimeManager.myActionPoints >= 0 && myAPcost <= thisPlayerTimeManager.myActionPoints){
				if(GetComponentInParent<PlayerIdentifier>().myPlayerNum == 0){
					Jump(jumpKey);
					// thisPlayerTimeManager.
				} else if(GetComponentInParent<PlayerIdentifier>().myPlayerNum == 1){
					Jump(P2_jumpKey);
				}
			}
		}
	}

	public void Jump(KeyCode key){
		fpc.m_Jump = Input.GetKeyDown(key);
		isJumping = Input.GetKeyDown(key);
  	}
}
