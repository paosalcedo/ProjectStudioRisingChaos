using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeControl : MonoBehaviour {

	public GameObject grenade;
	protected PlayerTimeManager currentPlayerTimeManager;
	protected PlayerTimeManager thisPlayerTimeManager;
	protected KeyCode attackKey;
	public float cooldown = 0;
	protected float startingCooldown;
	protected float myAPcost; 
	// Vector3 modPos = Vector3.zero;
	// Use this for initialization
	public enum FiringState{
		FIRING,
		NOT_FIRING
	}
	void Start () {
		grenade.SetActive(true);
		startingCooldown = cooldown;
		currentPlayerTimeManager = CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>();
		thisPlayerTimeManager = GetComponentInParent<PlayerTimeManager>();
		attackKey = KeyCode.Mouse0;
		myAPcost = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].ap_cost;
		startingCooldown = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].cooldown;
		cooldown = 0;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(thisPlayerTimeManager.playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
			if(thisPlayerTimeManager.myActionPoints >= 0 && thisPlayerTimeManager.ap_attackCost <= thisPlayerTimeManager.myActionPoints){
				Attack(attackKey);	
			}
		}
		
	}
	public virtual void Attack(KeyCode key){
		if(cooldown > 0){
			cooldown -= Time.deltaTime;
		}
		// cooldown -= Time.deltaTime;
		else if(cooldown <= 0){
			cooldown = 0;
		}

		if(Input.GetKeyDown(key) && cooldown <= 0){
			GameObject grenade;
			grenade = Instantiate (Services.Prefabs.Grenade) as GameObject;
			grenade.transform.position = transform.position + transform.forward;
			grenade.transform.rotation = transform.rotation;
			thisPlayerTimeManager.myActionPoints -= thisPlayerTimeManager.ap_attackCost;  
			cooldown = startingCooldown;
 		}
  	}
}
