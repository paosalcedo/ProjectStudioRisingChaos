using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	public bool hasBeenPickedUp = false;
	public enum PickupType{
		Weapon,
		Ammo,
		Powerup
	}

	public float respawnTime;
	public PickupType pickupType;
	public WeaponType myWeaponType;

	private Component[] renderers;
	private Component[] colliders;
	WeaponAndAmmoManager playerWpnManager;
	// Use this for initialization
	void Start () {
 	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){
		if(coll.GetComponent<WeaponAndAmmoManager>() != null && !hasBeenPickedUp){
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
				default:
				break;
			}
			TogglePickupActive();
			StartCoroutine(RespawnPickup(respawnTime));
			// renderers = GetComponentsInChildren<MeshRenderer>();
			// foreach (MeshRenderer renderer in renderers){
			// 	renderer.enabled = false;
			// }		
			// colliders = GetComponentsInChildren<Collider>();
			// foreach(Collider collider in colliders){
			// 	collider.enabled = false;
			// }	
			// GetComponentInChildren<Collider>().enabled = false;
			hasBeenPickedUp = true;
		} 
	}

	void TogglePickupActive(){
		renderers = GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer renderer in renderers){
			renderer.enabled = !renderer.enabled;
		}		
		colliders = GetComponentsInChildren<Collider>();
		foreach(Collider collider in colliders){
			collider.enabled = !collider.enabled;
		}
	}

	IEnumerator RespawnPickup(float respawnTime_){
		yield return new WaitForSeconds(respawnTime_);
		TogglePickupActive();
		hasBeenPickedUp = false;
	}

}
