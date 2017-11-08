﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEngine : MonoBehaviour {

	public GameObject gameObjectToIgnore;
	private Rigidbody rb;
	private float speed;
	private int damage;
	bool trapTriggered;

	void Awake(){
		rb = GetComponentInParent<Rigidbody>();
		speed = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Trap].speed;
		damage = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Trap].damage;
	}
	void Start () {
		trapTriggered = false;

		// GetComponent<Collider>().enabled = false;
		MoveTrap();
		// StartCoroutine(SetTrapToActive(0.5f));
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(Vector3.down);
	}

	void OnTriggerEnter(Collider trapped){
		if(trapped.GetComponent<PlayerTimeManager>() != null 
			&& !trapTriggered 
			&& !trapped.gameObject.name.Contains("Slowtrap")
			&& trapped.gameObject != gameObjectToIgnore){

  			trapped.GetComponent<PlayerTimeManager>().myActionPoints = 10;
			trapped.GetComponent<PlayerHealthManager>().currentHealth -= damage;
			trapTriggered = true;
			Destroy(gameObject, 0.01f);
			Destroy(transform.parent.gameObject, 0.02f);
		}
	}

	void MoveTrap(){
		rb.AddForce(transform.forward * speed, ForceMode.Impulse);
	}

	IEnumerator SetTrapToActive(float delay){
		yield return new WaitForSeconds(delay);
		GetComponent<Collider>().enabled = true;
	}

	public void GetGameObjectToIgnore(GameObject ignoreThis){
 		gameObjectToIgnore = ignoreThis;		
	}
}
