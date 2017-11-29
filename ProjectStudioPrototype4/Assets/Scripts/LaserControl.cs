using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.CrossPlatformInput;
public class LaserControl : MonoBehaviour {

	public GameObject weaponSounds;
	public WeaponSoundManager weaponSoundManager;
	public GameObject laserPistol;
	protected PlayerTimeManager thisPlayerTimeManager;
	StealthPlayerSwitcher playerSwitcher;
	public float laserLifetime = 1f;

	protected float myAPcost;
	protected int damage;
	Transform parent;
	public KeyCode attackKey;
	public KeyCode p2_attackKey;
	float laserLifetimeReset;
	// Use this for initialization
	protected virtual void Start () {
		weaponSoundManager = weaponSounds.GetComponent<WeaponSoundManager>();
		laserPistol.SetActive(true);
		playerSwitcher = GetComponentInParent<StealthPlayerSwitcher>();
		thisPlayerTimeManager = GetComponentInParent<PlayerTimeManager>();
		parent = transform.parent;
		laserLifetimeReset = laserLifetime;
		damage = Services.WeaponDefinitions.weapons[WeaponType.Laser].damage;
		myAPcost = Services.WeaponDefinitions.weapons[WeaponType.Laser].ap_cost;
	}
	
	// Update is called once per frame
	public virtual void Update () {
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

	// private void ControllerAttack(){
	// 	if (CrossPlatformInputManager.GetButtonDown("Fire1")){
	// 		// ShootRay();
	// 		GameObject reflectoid = Instantiate(Services.Prefabs.Reflectoid, transform.position + (transform.forward * 2), Quaternion.identity) as GameObject;
	// 		reflectoid.transform.rotation = transform.rotation;
	// 		thisPlayerTimeManager.myActionPoints -= myAPcost; 
	// 		reflectoid.GetComponent<ReflectoidEngine>().playerWhoFiredMe = transform.parent.gameObject;
	// 		weaponSoundManager.PlayLaserSound();	
 	// 	}
	// }

	public virtual void Attack(KeyCode key){
		// Debug.Log("Firing laser!");
		if (Input.GetKeyDown(key)){
			Debug.Log("Using laser!");
			// ShootRay();
			GameObject reflectoid = Instantiate(Services.Prefabs.Reflectoid, transform.position + (transform.forward * 2), Quaternion.identity) as GameObject;
			reflectoid.transform.rotation = transform.rotation;
			thisPlayerTimeManager.myActionPoints -= myAPcost; 
			reflectoid.GetComponent<ReflectoidEngine>().playerWhoFiredMe = transform.parent.gameObject;
			weaponSoundManager.PlayLaserSound();	
 		}
	}

	void AnimateLaserAttack(){
		
	}

}
