using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDefinitions {

	public enum WeaponType{
		Grenade,
		Trap,
		Mine
	
	}

	WeaponType weapon;

	public Dictionary<WeaponType, WeaponInfo> weapons = new Dictionary<WeaponType, WeaponInfo>(){
		{ 
			WeaponType.Grenade, new WeaponInfo (
				"a grenade for throwing", 
				20f, 
				30, 
				2, 
				"unknown"
			) 
		},
		{ 
			WeaponType.Trap, new WeaponInfo (
				"a souvenir for a friend, if you hate that friend", 
				100f, 
				15, 
				5f, 
				"who knows"
			)
		}
	};

}	
