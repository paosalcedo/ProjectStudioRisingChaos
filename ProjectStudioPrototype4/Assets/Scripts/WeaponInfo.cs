using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo {

	public string description;
	public float speed;
	public int damage;
	public float cooldown;
	public int ap_cost;

	public int baseAmmoCount;

	public int ammoPickUpCount; 
	public string path; 


	public WeaponInfo(string description_, float speed_, int damage_, float cooldown_, int ap_cost_, int baseAmmoCount_, int ammoPickUpCount_, string path_){
		description = description_;
		speed = speed_;
		damage = damage_;
		cooldown = cooldown_;
		ap_cost = ap_cost_;
		baseAmmoCount = baseAmmoCount_;
		ammoPickUpCount = ammoPickUpCount_;
		path = path_;
	}

}
