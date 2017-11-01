using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEngine : MonoBehaviour {


	Rigidbody rb;
	private float radius = 2.5F;
    // private float power = 20.0F;	
	private int damage;

	private bool exploded;

	private float speed;
	private float upwardsMod = 0f;
	private float timeBeforeExplode = 2f;
	void Awake(){
		rb = GetComponent<Rigidbody>();
		speed = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].speed;
		damage = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].damage;
	}
	void Start () {
		exploded = false;
		MoveGrenade();
		// Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		StartCoroutine(DelayedExplosion(timeBeforeExplode));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
 	}

	public void MoveGrenade(){
		rb.AddForce(transform.forward * speed, ForceMode.Impulse);
	}

	IEnumerator DelayedExplosion(float delay){
		yield return new WaitForSeconds(delay);
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		foreach (Collider hit in colliders){
			if(	  
				hit.tag == "Player" 
				// && !exploded
			){
				//check if player within explosion is the other player. 
				if(hit.GetComponent<PlayerTimeManager>().playerFrozenState == PlayerTimeManager.PlayerFrozenState.Frozen){
					//if so, deplete health.
					// Debug.Log("Depleting health on " + hit.transform.GetComponent<PlayerIdentifier>().myName);
					hit.GetComponent<PlayerHealthManager>().DepleteHealth(damage);
					//check if hit player is currentPlayer or otherPlayer.
					if(hit.gameObject == CurrentPlayerTracker.otherPlayer){
					//tell the canvas of currentPlayer to show a hit alert.
						CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(hit.GetComponent<PlayerIdentifier>().myName, damage);
					} else {
						CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(hit.GetComponent<PlayerIdentifier>().myName, damage);
					}
				} else {
					Debug.Log("Found no target!");
				}
			}
		}
		// exploded = true;
		Destroy(gameObject);
	}
}
