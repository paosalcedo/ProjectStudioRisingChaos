using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAndAmmoManager : MonoBehaviour {

	public bool hasGrenade;
	public bool hasLaser;
	public bool hasTrap;

	public int grenadeAmmoCount;
	public int laserAmmoCount;
	public int trapAmmoCount; 
	// Use this for initialization
	void Start () {
		grenadeAmmoCount = 0;
		laserAmmoCount = 0;
		trapAmmoCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PickupWeapon(WeaponType weaponType){
		switch (weaponType){
			case WeaponType.Laser:  
				Debug.Log("Picked up a " + weaponType);
				if(!hasLaser){
					hasLaser = true;
					laserAmmoCount = Services.WeaponDefinitions.weapons[weaponType].baseAmmoCount;
				} else {
					laserAmmoCount += Services.WeaponDefinitions.weapons[weaponType].baseAmmoCount;
				}
			break;
			case WeaponType.Grenade:
				Debug.Log("Picked up a " + weaponType);
				hasGrenade = true;
				grenadeAmmoCount = Services.WeaponDefinitions.weapons[weaponType].baseAmmoCount;
			break;
			case WeaponType.Trap:
				Debug.Log("Picked up a " + weaponType);
				hasTrap = true;
				grenadeAmmoCount = Services.WeaponDefinitions.weapons[weaponType].baseAmmoCount;
				break;
			default:
			break;			
		}
	}

	public void PickupAmmo(WeaponType weaponType){
		switch (weaponType){
			case WeaponType.Laser:  
				Debug.Log("Picked up ammo for " + weaponType);
				hasLaser = true;
				laserAmmoCount = Services.WeaponDefinitions.weapons[weaponType].baseAmmoCount;
			break;
			case WeaponType.Grenade:
				Debug.Log("Picked up ammo for " + weaponType);
				hasGrenade = true;
				grenadeAmmoCount = Services.WeaponDefinitions.weapons[weaponType].baseAmmoCount;
			break;
			case WeaponType.Trap:
				Debug.Log("Picked up ammo for " + weaponType);
				hasTrap = true;
				grenadeAmmoCount = Services.WeaponDefinitions.weapons[weaponType].ammoPickUpCount;
				break;
			default:
			break;			
		}
	}

	
}
