using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameragunControl : GrenadeControl {

	// Use this for initialization
	[SerializeField] int cameraCount = 1;
	private int myPlayerNum;
	void Start () {
		firstPersonModel.SetActive(true);
		startingCooldown = cooldown;
		if(GetComponentInParent<PlayerIdentifier>().myPlayerNum == 0){
			attackKey = KeyCode.Joystick1Button7;
		} else if (GetComponentInParent<PlayerIdentifier>().myPlayerNum == 1){
			attackKey = KeyCode.Joystick2Button7;
		}
		currentPlayerTimeManager = CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>();
		thisPlayerTimeManager = GetComponentInParent<PlayerTimeManager>();
		startingCooldown = Services.WeaponDefinitions.weapons[WeaponType.Cameragun].cooldown;
		myAPcost = Services.WeaponDefinitions.weapons[WeaponType.Cameragun].ap_cost;
		cooldown = 0;
		myPlayerNum = GetComponentInParent<PlayerIdentifier>().myPlayerNum;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public override void Attack(KeyCode key){
		if(Input.GetKeyDown(key) && cameraCount > 0){
			GameObject camera;
			Debug.Log("Firing camera!");
			camera = Instantiate (Services.Prefabs.CameraProjectile) as GameObject;
			camera.GetComponent<CameraProjectile>().Setup(WeaponType.Cameragun);
			camera.transform.position = transform.position + transform.forward * 2;
			camera.transform.rotation = transform.rotation;
			// camera.GetComponent<cameraEngine>().colliderToIgnore = transform.parent.GetComponent<CapsuleCollider>();
			if(GetComponentInParent<PlayerIdentifier>().myPlayerNum == 0){
				camera.layer = 8;	
			} else if (GetComponentInParent<PlayerIdentifier>().myPlayerNum == 1){
				camera.layer = 9;
			}
			// add more else ifs if there are more players
			thisPlayerTimeManager.myActionPoints -= myAPcost;  
			cameraCount = 0;
 		}
	}

	public void ResetCameraCount(){
		cameraCount = 1;
	}
}
