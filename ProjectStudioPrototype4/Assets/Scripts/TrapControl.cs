using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : GrenadeControl {

	public GameObject trap;
	int myPlayerNum;

 	void Start () {
		trap.SetActive(true);
		startingCooldown = cooldown;
		currentPlayerTimeManager = CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>();
		thisPlayerTimeManager = GetComponentInParent<PlayerTimeManager>();
		attackKey = KeyCode.Mouse0;
		startingCooldown = Services.WeaponDefinitions.weapons[WeaponType.Trap].cooldown;
		myAPcost = Services.WeaponDefinitions.weapons[WeaponType.Trap].ap_cost;
		cooldown = 0;
		myPlayerNum = GetComponentInParent<PlayerIdentifier>().myPlayerNum;
		if(myPlayerNum == 0){
			
		}
	}

	public override void Update(){
		base.Update();
	}

	public override void Attack(KeyCode key){
		Debug.Log("Throwing trap!");
		if(cooldown > 0){
			cooldown -= Time.deltaTime;
		}
		// cooldown -= Time.deltaTime;
		else if(cooldown <= 0){
			cooldown = 0;
		}

		if(Input.GetKeyDown(key) && cooldown <= 0){
			GameObject trap;
			trap = Instantiate (Services.Prefabs.Trap) as GameObject;
			trap.transform.position = transform.position + transform.forward * 2;
			trap.transform.rotation = transform.rotation;
			// trap.GetComponent<TrapEngine>().colliderToIgnore = transform.parent.GetComponent<CapsuleCollider>();
			if(GetComponentInParent<PlayerIdentifier>().myPlayerNum == 0){
				trap.layer = 8;	
			} else if (GetComponentInParent<PlayerIdentifier>().myPlayerNum == 1){
				trap.layer = 9;
			}
			// add more else ifs if there are more players
			trap.GetComponentInChildren<TrapEngine>().GetGameObjectToIgnore(transform.parent.gameObject);
			thisPlayerTimeManager.myActionPoints -= myAPcost;  
			cooldown = startingCooldown;
 		}
	}


}
