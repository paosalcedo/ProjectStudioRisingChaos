using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo {

	public string description;
	public float speed;
	public int damage;
	public float cooldown;
	public string path; 

	public WeaponInfo(string description_, float speed_, int damage_, float cooldown_, string path_){
		description = description_;
		speed = speed_;
		damage = damage_;
		cooldown = cooldown_;
		path = path_;
	}

}
