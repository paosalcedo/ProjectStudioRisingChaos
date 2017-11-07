using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : GrenadeControl {

	public GameObject trap;

 	void Start () {
		trap.SetActive(true);
		startingCooldown = cooldown;
		currentPlayerTimeManager = CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>();
		thisPlayerTimeManager = GetComponentInParent<PlayerTimeManager>();
		attackKey = KeyCode.Mouse0;
		startingCooldown = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Trap].cooldown;
		myAPcost = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Trap].ap_cost;
		cooldown = 0;
	}

	public override void Update(){
		base.Update();
	}

	public override void Attack(KeyCode key){
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
			trap.transform.position = transform.position + transform.forward;
			trap.transform.rotation = transform.rotation;
			// trap.GetComponent<TrapEngine>().colliderToIgnore = transform.parent.GetComponent<CapsuleCollider>();
			trap.GetComponentInChildren<TrapEngine>().GetGameObjectToIgnore(transform.parent.gameObject);
			thisPlayerTimeManager.myActionPoints -= myAPcost;  
			cooldown = startingCooldown;
 		}
	}
}
