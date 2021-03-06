﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEngine : MonoBehaviour {

	AudioSource audioSource;
	Rigidbody rb;
	public AudioClip bounceClip;
	public AudioClip explodeClip;
	private float radius = 3.5F;
    // private float power = 20.0F;	
	private int damage;

	private bool exploded;

	private float speed;
	private float upwardsMod = 0f;
	private float timeBeforeExplode = 2f;
	void Awake(){
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		speed = Services.WeaponDefinitions.weapons[WeaponType.Grenade].speed;
		damage = Services.WeaponDefinitions.weapons[WeaponType.Grenade].damage;
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
		audioSource.PlayOneShot(explodeClip);
		audioSource.pitch = Random.Range(0.75f, 1);
		transform.GetChild(0).gameObject.SetActive(false);
		GameObject grenadeParticles = Instantiate(Services.Prefabs.GrenadeParticles, transform.position, transform.rotation) as GameObject;
		// audioSource.PlayOneShot(explodeClip);
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
					if(hit.GetComponent<PlayerHealthManager>().currentHealth > 0){
						if(hit.gameObject == CurrentPlayerTracker.otherPlayer){
						//tell the canvas of currentPlayer to show a hit alert.
							CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(hit.GetComponent<PlayerIdentifier>().myName, damage);
						//tell otherPlayer to show UpdateGotHitAlert.
							CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.currentPlayer.transform.GetComponent<PlayerIdentifier>().myName, damage);
						} else {
						//tell the canvas of otherPlayer to show a hit alert.
							CurrentPlayerTracker.otherPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateHitAlert(hit.GetComponent<PlayerIdentifier>().myName, damage);
						//tell otherPlayer to show UpdateGotHitAlert.	
							CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().myCanvas.GetComponent<PlayerCanvasUpdater>().UpdateGotHitAlert(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName, damage);
						}
					} else {
						//if player 2 died
						if(hit.gameObject == CurrentPlayerTracker.otherPlayer){
							CurrentPlayerTracker.currentPlayer.GetComponentInChildren<PlayerCanvasUpdater>().UpdateAlertTextWithFrag(CurrentPlayerTracker.otherPlayer.GetComponent<PlayerIdentifier>().myName);
						} 
						//if player 1 died
						else {
							CurrentPlayerTracker.otherPlayer.GetComponentInChildren<PlayerCanvasUpdater>()
							.UpdateAlertTextWithFrag(CurrentPlayerTracker.currentPlayer.GetComponent<PlayerIdentifier>().myName);
						}				
					}
				} else {
					Debug.Log("Found no target!");
				}
			}
		}
		// exploded = true;
		Destroy(grenadeParticles, explodeClip.length*0.75f);
		Destroy(gameObject, explodeClip.length);
	
	}
	double delay = 0.000001;
	void OnCollisionEnter(){
		Debug.Log("Should be playing grenade bounce now!");
		audioSource.pitch = Random.Range(0.75f, 1);
		audioSource.clip = bounceClip;
		audioSource.PlayScheduled(AudioSettings.dspTime + delay);
	}
}
