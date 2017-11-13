using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEngine : MonoBehaviour {

	Collider myCollider;
	GameObject parent;
	private int damage;
	// Use this for initialization
	void Start () {
		damage = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Knife].damage;
		parent = transform.parent.gameObject;
		myCollider = GetComponent<BoxCollider>();
		myCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnableKnifeCollider(){
		myCollider.enabled = true;
	}

	public void DisableKnifeCollider(){
		myCollider.enabled = false;
	}

	void OnTriggerEnter(Collider coll){
		//check if the thing you're slicing is the player holding the knife.
		if(coll.gameObject.tag == "Player" && coll.gameObject != parent.transform.parent.gameObject) {
			Debug.Log("hit the other player!");
			coll.gameObject.GetComponent<PlayerHealthManager>().DepleteHealth(damage);
			//if target player still has health, only print hit messages
			if(coll.GetComponent<PlayerHealthManager>().currentHealth > 0){
				if(coll.gameObject == CurrentPlayerTracker.otherPlayer){
					//tell the canvas of currentPlayer to show a hit alert.
					CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(coll.transform.GetComponent<PlayerIdentifier>().myName, damage);
					CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.currentPlayer.transform.GetComponent<PlayerIdentifier>().myName, damage);

				} else {
					CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(coll.transform.GetComponent<PlayerIdentifier>().myName, damage);
					CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName, damage);			
				}
			}
			//if target player's hp hit 0, print different message
			else {
				//if player 2 died
				if(coll.gameObject == CurrentPlayerTracker.otherPlayer){
					CurrentPlayerTracker.currentPlayer.GetComponentInChildren<PlayerCanvasUpdater>().UpdateAlertTextWithFrag(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName);
				} 
				//if player 1 died
				else {
					CurrentPlayerTracker.otherPlayer.GetComponentInChildren<PlayerCanvasUpdater>()
					.UpdateAlertTextWithFrag(CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myName);
				}
			}

		}
	}
}
