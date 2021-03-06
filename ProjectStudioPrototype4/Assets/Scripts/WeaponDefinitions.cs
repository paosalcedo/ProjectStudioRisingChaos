﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDefinitions {

	// public enum WeaponType{
	// 	Grenade,
	// 	Trap,
	// 	Mine,
	// 	Laser,
	// 	Knife

	// }

	WeaponType weapon;

	public Dictionary<WeaponType, WeaponInfo> weapons = new Dictionary<WeaponType, WeaponInfo>(){
		{ 
			WeaponType.Grenade, new WeaponInfo (
				"a grenade for throwing", 
				14f, //speed
				30, //damage
				2, //cooldown
				110, //ap cost
				3,//base ammo
				2,//
				"unknown"
			) 
		},
		{ 
			WeaponType.Trap, new WeaponInfo (
				"a souvenir for a friend, if you hate that friend", 
				1, 
				10, 
				5f,
				110,
				2,
				2,
				"who knows"
			)
		},
		{ 
			WeaponType.Laser, new WeaponInfo (
				"railgun, sniper rifle, call it what you want. it hits instantly where you point it at and it hits hard.", 
				Mathf.Infinity, 
				40,  
				2.5f,
				110,
				6,//base ammo on pickup
				3,  //ammo box pickup
				"who knows"
			)
		},
		{ 
			WeaponType.Knife, new WeaponInfo (
				"A friendly knifey", 
				30, 
				100,  
				5f,
				110,
				0,
				0,  
				"who knows"
			)
		},
		{
			WeaponType.Cameragun, new WeaponInfo (
				"A recon weapon", 
				20, 
				0,  
				5f,
				110,
				2,
				1,  
				"who knows"
			)
		}
	};

}	
