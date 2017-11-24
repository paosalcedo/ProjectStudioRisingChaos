using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
	float laserLifetimeReset;
	// Use this for initialization
	protected virtual void Start () {
		weaponSoundManager = weaponSounds.GetComponent<WeaponSoundManager>();
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
		// Debug.Log("Firing laser!");
		if (Input.GetKeyDown(key)){
			// ShootRay();
			GameObject reflectoid = Instantiate(Services.Prefabs.Reflectoid, transform.position + (transform.forward * 2), Quaternion.identity) as GameObject;
			reflectoid.transform.rotation = transform.rotation;
			thisPlayerTimeManager.myActionPoints -= myAPcost; 
			reflectoid.GetComponent<ReflectoidEngine>().playerWhoFiredMe = transform.parent.gameObject;
			weaponSoundManager.PlayLaserSound();	
 		}
	}

	/*public void ShootRay(){
		Ray ray = new Ray(transform.position, transform.forward);

		RaycastHit rayHit = new RaycastHit();
		Debug.DrawRay(transform.position, transform.forward * 100f, Color.red, 3f);
		thisPlayerTimeManager.myActionPoints -= myAPcost; 
	
		// reflectoid.GetComponent<ReflectoidEngine>().ShootRay();
		// reflectoid.GetComponent<ReflectoidEngine>().GetInitialDirection(transform.forward);
		if(Physics.Raycast(ray, out rayHit, Mathf.Infinity)){
			Vector3 firstReflection = Vector3.Reflect(ray.direction, rayHit.normal);
			Debug.DrawRay (rayHit.point, firstReflection * Mathf.Infinity, Color.blue);
			if(	  
				rayHit.transform.tag == "Player" 
				// && !exploded
			){	
				//check if player hit by raycast is the other player. 
				if(rayHit.transform.GetComponent<PlayerTimeManager>().playerFrozenState == PlayerTimeManager.PlayerFrozenState.Frozen){
					//if so, deplete health.
					// Debug.Log("Depleting health on " + rayHit.transform.GetComponent<PlayerIdentifier>().myName);
					// rayHit.transform.GetComponent<PlayerHealthManager>().DepleteHealth(damage);

					//since we now know the hit target is the other player, check if rayHit player is Player 2.
					if(rayHit.transform.gameObject == CurrentPlayerTracker.otherPlayer){
						//check if Player 2 has any health left.
						if(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerHealthManager>().currentHealth > 0){
						//IF Player 2 has some health left, tell the canvas of currentPlayer to show a hit alert.
							CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(rayHit.transform.GetComponent<PlayerIdentifier>().myName, damage);
							CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.currentPlayer.transform.GetComponent<PlayerIdentifier>().myName, damage);
						} 
						//if Player 2 got killed by the laser, show Player 1 that they killed Player 2.
						else {
							CurrentPlayerTracker.currentPlayer.GetComponentInChildren<PlayerCanvasUpdater>().UpdateAlertTextWithFrag(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName);
						}
					} 
					//if it's not Player 2, then it's player 1 who got hit.
					else {
						if(CurrentPlayerTracker.currentPlayer.GetComponent<PlayerHealthManager>().currentHealth > 0){
						//tell the canvas of currentPlayer to show a hit alert.
							CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(rayHit.transform.GetComponent<PlayerIdentifier>().myName, damage);
							CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName, damage);
						} 
						//if Player 2 got killed by the laser, show Player 1 that they killed Player 2.
						else {
							CurrentPlayerTracker.otherPlayer.GetComponentInChildren<PlayerCanvasUpdater>().UpdateAlertTextWithFrag(CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myName);
						}
					}
				} 
			} else if (rayHit.transform.tag != "Player"){
				//first ricochet.
 				Ray secondRay = new Ray(rayHit.point, firstReflection);
				RaycastHit secondRayHit = new RaycastHit();
				Debug.DrawRay(rayHit.point, firstReflection * 100f, Color.blue, 3f);			
				// reflectoid.transform.DOMove(secondRayHit.point, 0.5f, false);
				if(Physics.Raycast(secondRay, out secondRayHit, Mathf.Infinity)){
					if(secondRayHit.transform.tag != "Player"){
						Vector3 secondReflection = Vector3.Reflect(secondRay.direction, secondRayHit.normal);
						Ray thirdRay = new Ray(secondRayHit.point, secondReflection);
						RaycastHit thirdRayHit = new RaycastHit();	
						Debug.DrawRay(secondRayHit.point, secondReflection * 100f, Color.green, 3f);
						if(Physics.Raycast(thirdRay, out thirdRayHit, Mathf.Infinity)){
							if(thirdRayHit.transform.tag != "Player"){
								Debug.Log("Hit something with third ray!");
							} else {
								Debug.Log("Hit player with third ray!");
								// thirdRayHit.transform.GetComponent<PlayerHealthManager>().DepleteHealth(damage);

							}
						}
					} 
					//if reflection hit the player
					else {
						// secondRayHit.transform.GetComponent<PlayerHealthManager>().DepleteHealth(damage);
						if(secondRayHit.transform.gameObject == CurrentPlayerTracker.otherPlayer){
 							if(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerHealthManager>().currentHealth > 0){
 								CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(secondRayHit.transform.GetComponent<PlayerIdentifier>().myName, damage);
								CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.currentPlayer.transform.GetComponent<PlayerIdentifier>().myName, damage);
							} 
 							else {
								CurrentPlayerTracker.currentPlayer.GetComponentInChildren<PlayerCanvasUpdater>().UpdateAlertTextWithFrag(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName);
							}
						} 
 						else {
							if(CurrentPlayerTracker.currentPlayer.GetComponent<PlayerHealthManager>().currentHealth > 0){
 								CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(secondRayHit.transform.GetComponent<PlayerIdentifier>().myName, damage);
								CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName, damage);
							} 
 							else {
								// CurrentPlayerTracker.otherPlayer.GetComponentInChildren<PlayerCanvasUpdater>().UpdateAlertTextWithFrag(CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myName);
							}
						}
					}
				} 

			}
			 else {
				Debug.Log("Hit nothing!");
				thisPlayerTimeManager.myActionPoints -= myAPcost;  
			}	
		}
	}*/

	void AnimateLaserAttack(){
		
	}

}
