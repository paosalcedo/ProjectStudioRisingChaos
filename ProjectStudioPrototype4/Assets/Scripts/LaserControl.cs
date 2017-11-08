using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour {

	public GameObject laserPistol;
	protected PlayerTimeManager thisPlayerTimeManager;
	StealthPlayerSwitcher playerSwitcher;
	public float laserLifetime = 1f;

	protected float myAPcost;
	protected int damage;
	Transform parent;
	public KeyCode attackKey;
	float laserLifetimeReset;
	// Use this for initialization
	protected virtual void Start () {
		laserPistol.SetActive(true);
		playerSwitcher = GetComponentInParent<StealthPlayerSwitcher>();
		thisPlayerTimeManager = GetComponentInParent<PlayerTimeManager>();
		parent = transform.parent;
		laserLifetimeReset = laserLifetime;
		attackKey = KeyCode.Mouse0;
		damage = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Laser].damage;
		myAPcost = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Laser].ap_cost;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(thisPlayerTimeManager.playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
			if(thisPlayerTimeManager.myActionPoints >= 0 && myAPcost <= thisPlayerTimeManager.myActionPoints){
				Attack(attackKey);	
			}
		}
	}

	public virtual void Attack(KeyCode key){
		Debug.Log("Firing laser!");
		if (Input.GetKeyDown(key)){
			ShootRay();
 		}
	}
	
	public void ShootRay(){
		Ray ray = new Ray(transform.position, transform.forward);

		RaycastHit rayHit = new RaycastHit();
		Debug.DrawRay(transform.position, transform.forward * 10f, Color.red, 3f);
		if(Physics.Raycast(ray, out rayHit, Mathf.Infinity)){
			if(	  
				rayHit.transform.tag == "Player" 
				// && !exploded
			){
				//check if player within explosion is the other player. 
				if(rayHit.transform.GetComponent<PlayerTimeManager>().playerFrozenState == PlayerTimeManager.PlayerFrozenState.Frozen){
					//if so, deplete health.
					// Debug.Log("Depleting health on " + rayHit.transform.GetComponent<PlayerIdentifier>().myName);
					rayHit.transform.GetComponent<PlayerHealthManager>().DepleteHealth(damage);
					thisPlayerTimeManager.myActionPoints -= myAPcost;  

					//check if rayHit player is currentPlayer or otherPlayer.
					if(rayHit.transform.gameObject == CurrentPlayerTracker.otherPlayer){
					//tell the canvas of currentPlayer to show a hit alert.
						CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(rayHit.transform.GetComponent<PlayerIdentifier>().myName, damage);
					} else {
						CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(rayHit.transform.GetComponent<PlayerIdentifier>().myName, damage);
					}
				} 
			}
			 else {
				Debug.Log("Hit nothing!");
				thisPlayerTimeManager.myActionPoints -= myAPcost;  
			}	
		}
	}

	void AnimateLaserAttack(){
		
	}

}
