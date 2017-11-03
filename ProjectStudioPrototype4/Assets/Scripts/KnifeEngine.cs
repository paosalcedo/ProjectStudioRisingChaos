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
			if(coll.gameObject == CurrentPlayerTracker.otherPlayer){
				//tell the canvas of currentPlayer to show a hit alert.
				CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(coll.transform.GetComponent<PlayerIdentifier>().myName, damage);
			} else {
				CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(coll.transform.GetComponent<PlayerIdentifier>().myName, damage);
			}
		}
	}
}
