using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;

public class GrenadeControl : MonoBehaviour {

	public GameObject firstPersonModel;

	public WeaponSoundManager weaponSoundManager;
	protected PlayerTimeManager currentPlayerTimeManager;
	protected PlayerTimeManager thisPlayerTimeManager;
	public KeyCode attackKey;

	public KeyCode p2_attackKey;
	[SerializeField]private GameObject pin;
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
		firstPersonModel.SetActive(true);
		if(GetComponentInParent<PlayerIdentifier>().myPlayerNum == 0){
			attackKey = KeyCode.Joystick1Button7;
		} else if (GetComponentInParent<PlayerIdentifier>().myPlayerNum == 1){
			attackKey = KeyCode.Joystick2Button7;
		}
		startingCooldown = cooldown;
		currentPlayerTimeManager = CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>();
		thisPlayerTimeManager = GetComponentInParent<PlayerTimeManager>();
		myAPcost = Services.WeaponDefinitions.weapons[WeaponType.Grenade].ap_cost;
		startingCooldown = Services.WeaponDefinitions.weapons[WeaponType.Grenade].cooldown;
		cooldown = 0;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		// if(thisPlayerTimeManager.playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
		// 	if(thisPlayerTimeManager.myActionPoints >= 0 && thisPlayerTimeManager.ap_attackCost <= thisPlayerTimeManager.myActionPoints){
		// 		Attack(attackKey);	
		// 		// ControllerAttack();
		// 	}
		// }
		if(thisPlayerTimeManager.playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
			if(thisPlayerTimeManager.myActionPoints >= 0 && myAPcost <= thisPlayerTimeManager.myActionPoints){
				if(GetComponentInParent<PlayerIdentifier>().myPlayerNum == 0){
					Attack(attackKey);
				} else if(GetComponentInParent<PlayerIdentifier>().myPlayerNum == 1){
					Attack(p2_attackKey);
				}
			}
		}
	}
	public virtual void Attack(KeyCode key){

		Debug.Log("Throwing grenade!");
		if(cooldown > 0){
			cooldown -= Time.deltaTime;
		}
		// cooldown -= Time.deltaTime;
		else if(cooldown <= 0){
			cooldown = 0;
		}
		if(Input.GetKeyDown (key)){
			weaponSoundManager.PlayGrenadePullPin();
			pin.transform.DOLocalRotate(new Vector3 (90, 0 ,0), 0.1f, RotateMode.Fast);
			pin.transform.DOLocalMoveX(1, 0.25f, false);
		}

		if(Input.GetKeyUp (key) && cooldown <= 0){
			GameObject grenade;
			grenade = Instantiate (Services.Prefabs.Grenade) as GameObject;
			grenade.transform.position = transform.position + transform.forward;
			grenade.transform.rotation = transform.rotation;
			weaponSoundManager.PlayGrenadeThrowSound();
			thisPlayerTimeManager.myActionPoints -= thisPlayerTimeManager.ap_attackCost;  
			cooldown = startingCooldown;
 		}
  	}

	public virtual void ControllerAttack(){
		if(CrossPlatformInputManager.GetButtonDown("Fire1")){
			weaponSoundManager.PlayGrenadePullPin();
		}

		if(CrossPlatformInputManager.GetButtonUp("Fire1")){
			GameObject grenade;
			grenade = Instantiate (Services.Prefabs.Grenade) as GameObject;
			grenade.transform.position = transform.position + transform.forward;
			grenade.transform.rotation = transform.rotation;
			weaponSoundManager.PlayGrenadeThrowSound();
			thisPlayerTimeManager.myActionPoints -= thisPlayerTimeManager.ap_attackCost;  
			cooldown = startingCooldown;
		}

	}	
	public virtual void ResetCooldown(){
		cooldown = 0;
	}
}
