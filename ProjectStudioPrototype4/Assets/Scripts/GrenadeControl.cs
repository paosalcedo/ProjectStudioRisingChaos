using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeControl : MonoBehaviour {

	private PlayerTimeManager currentPlayerTimeManager;
	private PlayerTimeManager thisPlayerTimeManager;
	private KeyCode attackKey;
	public float cooldown = 0;
	private float startingCooldown;
	// Vector3 modPos = Vector3.zero;
	// Use this for initialization
	public enum FiringState{
		FIRING,
		NOT_FIRING
	}
	void Start () {
		currentPlayerTimeManager = CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>();
		thisPlayerTimeManager = GetComponentInParent<PlayerTimeManager>();
		attackKey = KeyCode.Mouse0;
		startingCooldown = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].cooldown;
		cooldown = startingCooldown;
	}
	
	// Update is called once per frame
	void Update () {
		// if(currentPlayerTimeManager != null){
		// 	if(currentPlayerTimeManager.playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
		// 		Debug.Log("attacking!");
		// 		if(currentPlayerTimeManager.myActionPoints >= 0 && currentPlayerTimeManager.ap_attackCost <= currentPlayerTimeManager.myActionPoints){
		// 			Attack(attackKey);	
		// 		}
		// 	}
		// }

		if(thisPlayerTimeManager.playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
			if(thisPlayerTimeManager.myActionPoints >= 0 && thisPlayerTimeManager.ap_attackCost <= thisPlayerTimeManager.myActionPoints){
				Attack(attackKey);	
			}
		}
		
	}
	public void Attack(KeyCode key){
		cooldown -= Time.deltaTime;
		if(Input.GetKeyDown(key) && cooldown <= 0){
			GameObject grenade;
			// GetComponentInParent<ActionRecorder>().isAttacking = true;
			grenade = Instantiate (Services.Prefabs.Grenade) as GameObject;
			grenade.transform.position = transform.position + transform.forward;
			grenade.transform.rotation = transform.rotation;
			thisPlayerTimeManager.myActionPoints -= thisPlayerTimeManager.ap_attackCost;  
			cooldown = startingCooldown;
 		}
  	}
}
