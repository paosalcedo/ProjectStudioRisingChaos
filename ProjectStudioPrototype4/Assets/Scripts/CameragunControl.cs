﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameragunControl : GrenadeControl {

	// Use this for initialization
	[SerializeField] int cameraCount = 1;

	[SerializeField] KeyCode retractCameraKey;
	private GameObject camera;
	public GameObject cameraModel;
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
		RetractCamera(retractCameraKey);
	}

	public override void Attack(KeyCode key){
		if(Input.GetKeyDown(key) && cameraCount > 0){
			cameraModel.SetActive(false);
			int _myPlayerNum;
			_myPlayerNum = GetComponentInParent<PlayerIdentifier>().myPlayerNum;
			GameObject _camera;
			_camera = Instantiate (Services.Prefabs.CameraProjectile) as GameObject;
			//tell the camera projectile which player fired it
			_camera.GetComponentInChildren<MouseLook>().myPlayerNum = _myPlayerNum;
			_camera.GetComponent<CameraProjectile>().Setup(WeaponType.Cameragun);
			_camera.transform.position = transform.position + transform.forward * 2;
			_camera.transform.rotation = transform.rotation;
			camera = _camera;
			// camera.GetComponent<cameraEngine>().colliderToIgnore = transform.parent.GetComponent<CapsuleCollider>();
			if(GetComponentInParent<PlayerIdentifier>().myPlayerNum == 0){
				_camera.layer = 8;	
			} else if (GetComponentInParent<PlayerIdentifier>().myPlayerNum == 1){
				_camera.layer = 9;
			}
			// add more else ifs if there are more players
			thisPlayerTimeManager.myActionPoints -= myAPcost;  
			cameraCount = 0;
		} 
	}

	private void RetractCamera(KeyCode key){
		if(Input.GetKeyDown(key)){
			//check if camera was actually tossed
			if(cameraCount == 0){
				cameraCount = 1;
				Sequence camReturnSequence = DOTween.Sequence();
				camReturnSequence.Append(camera.transform.DOMove(transform.position + Vector3.forward, 1f, false));
				camReturnSequence.OnComplete(()=>RestoreFirstPersonCameraAfterTween());
				// Destroy (camera);
			}
		}
	}

	private void RestoreFirstPersonCameraAfterTween(){
		Destroy(camera);
		cameraModel.SetActive(true);
	}
	public void ResetCameraCount(){
		cameraCount = 1;
	}

	private bool ToggleCameraModelIsActive(){
		bool cameraIsActive = false;
		cameraIsActive = !cameraIsActive;
		return cameraIsActive;
	}
}
