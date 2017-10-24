using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour {

	StealthPlayerSwitcher playerSwitcher;
	public float laserLifetime = 1f;
	private GameObject canvasDuringTurn;
	Transform parent;
	public KeyCode attackKey;
	float laserLifetimeReset;
	// Use this for initialization
	void Start () {
		playerSwitcher = GetComponentInParent<StealthPlayerSwitcher>();
		parent = transform.parent;
		laserLifetimeReset = laserLifetime;
		attackKey = KeyCode.Mouse0;
		canvasDuringTurn = GameObject.Find("CanvasDuringTurn");
	}
	
	// Update is called once per frame
	void Update () {
		if(CurrentPlayerTracker.currentPlayer == parent.gameObject){
			Attack(attackKey);
		}
	}

	public void Attack(KeyCode key){
		if (Input.GetKeyDown(key)){
			ShootRay();
		}
	}
	
	public void ShootRay(){
		Ray ray = new Ray(transform.position, transform.forward);

		RaycastHit rayHit = new RaycastHit();
		Debug.DrawRay(transform.position, transform.forward * 10f, Color.red, 3f);
		if(Physics.Raycast(ray, out rayHit, Mathf.Infinity)){
			if(rayHit.transform == playerSwitcher.otherPlayer.transform){
 				if(rayHit.transform.GetComponent<StealthPlayerSwitcher>().myIndex == 0){
					canvasDuringTurn.GetComponentInChildren<UpdateHitAlertText>().ShowHitAlert();
				} else if (rayHit.transform.GetComponent<StealthPlayerSwitcher>().myIndex == 1) {
					canvasDuringTurn.GetComponentInChildren<UpdateHitAlertText>().ShowHitAlert();
				}
				rayHit.transform.GetComponent<PlayerHealthManager>().DepleteHealth(34);
			} else {
				// Debug.Log("No one hit!");
			}
			Debug.Log(rayHit.transform.name);
		}
	}
}
