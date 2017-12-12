using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	public int turnsSincePickedUp;
	public int turnsUntilRespawn;
	public bool hasBeenPickedUp = false;
	public enum PickupType{
		Weapon,
		Ammo,
		Powerup,
		Health,
		Action_Points
	}

	public float respawnTime;
	public PickupType pickupType;
	public WeaponType myWeaponType;
	public int myValue;
	private Component[] renderers;
	private Component[] colliders;
	WeaponAndAmmoManager playerWpnManager;
	// Use this for initialization

	void Update(){
		if(turnsSincePickedUp >= turnsUntilRespawn){
			RespawnPickup();
			turnsSincePickedUp = 0;
		}
	}
	public virtual void OnTriggerEnter(Collider coll){
		if(	coll.GetComponent<WeaponAndAmmoManager>() != null 
			&& coll.GetComponent<PlayerTimeManager>() != null
			&& coll.GetComponent<PlayerHealthManager>() != null
			&& !hasBeenPickedUp){
			PlayerHealthManager playerHealthManager = coll.GetComponent<PlayerHealthManager>();
			PlayerTimeManager playerTimeManager = coll.GetComponent<PlayerTimeManager>();
 			playerWpnManager = coll.GetComponent<WeaponAndAmmoManager>();
			switch(pickupType){
				case PickupType.Weapon:
				playerWpnManager.PickupWeapon(myWeaponType);	
				break;
				case PickupType.Ammo:
				playerWpnManager.PickupAmmo(myWeaponType);
				break;
				case PickupType.Powerup:
				//add powerups/health pickups here
				break;
				case PickupType.Health:
				playerHealthManager.currentHealth += myValue;
				break;
				case PickupType.Action_Points:
				playerTimeManager.myActionPoints += myValue;
				break;
				default:
				break;
			}
			TogglePickupActive();
			// StartCoroutine(RespawnPickup(respawnTime));
			hasBeenPickedUp = true;
		} 
	}

	public virtual void TogglePickupActive(){
		renderers = GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer renderer in renderers){
			renderer.enabled = !renderer.enabled;
		}		
		colliders = GetComponentsInChildren<Collider>();
		foreach(Collider collider in colliders){
			collider.enabled = !collider.enabled;
		}
	}

	public virtual void IncrementTurnCounter(){
		turnsSincePickedUp++;
		Debug.Log("Incrementing turns on " + this.name);
	}

	// IEnumerator RespawnPickup(float respawnTime_){
	// 	yield return new WaitForSeconds(respawnTime_);
	// 	TogglePickupActive();
	// 	hasBeenPickedUp = false;
	// }

	void RespawnPickup(){
		// yield return new WaitForSeconds(respawnTime_);
		TogglePickupActive();
		hasBeenPickedUp = false;
	}


}
