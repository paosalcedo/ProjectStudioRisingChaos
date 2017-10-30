using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeControl : MonoBehaviour {

	private PlayerTimeManager playerTimeManager;
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
		playerTimeManager = GetComponentInParent<PlayerTimeManager>();
		attackKey = KeyCode.Mouse0;
		startingCooldown = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].cooldown;
		cooldown = startingCooldown;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerTimeManager != null){
			if(playerTimeManager.playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
				Debug.Log("attacking!");
				Attack(attackKey);	
			}
		}
	}
	public void Attack(KeyCode key){
		cooldown -= Time.deltaTime;
		if(Input.GetKeyDown(key) && cooldown <= 0){
			GameObject bullet;
			// GetComponentInParent<ActionRecorder>().isAttacking = true;
			bullet = Instantiate (Services.Prefabs.Grenade) as GameObject;
			bullet.transform.position = transform.position;
			bullet.transform.rotation = transform.rotation;
			cooldown = startingCooldown;
 		}
  	}
}
