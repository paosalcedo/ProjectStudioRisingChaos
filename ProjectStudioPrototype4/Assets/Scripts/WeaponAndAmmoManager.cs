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
		grenadeAmmoCount = Services.WeaponDefinitions.weapons[WeaponType.Grenade].baseAmmoCount;
		laserAmmoCount = Services.WeaponDefinitions.weapons[WeaponType.Laser].baseAmmoCount;
		trapAmmoCount = Services.WeaponDefinitions.weapons[WeaponType.Trap].baseAmmoCount;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
}
