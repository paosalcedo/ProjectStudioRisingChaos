using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class WeaponSwitcher : MonoBehaviour {

	public PlayerIdentifier playerIdentifier;
	WeaponAndAmmoManager weaponAndAmmoManager;
	public enum SelectedWeapon{
		Laser,
		Grenade,
		Trap,
		Knife
	}

	private PlayerCanvasUpdater playerCanvasUpdater;
	// public KeyCode selectTrapKey;
	// public KeyCode selectLaserKey;
	// public KeyCode selectGrenadeKey;
	// public KeyCode selectKnifeKey;
	private LaserControl laserControl;
	private GrenadeControl grenadeControl;
	private TrapControl trapControl;
	private KnifeControl knifeControl;
	private CameragunControl cameragunControl;
	private FirstPersonController fpc;
	public KeyCode p1_selectCameraKey = KeyCode.Joystick1Button4;
	public KeyCode p2_selectCameraKey = KeyCode.Joystick2Button4;

	// Use this for initialization
	void Start () {
		fpc = GetComponentInParent<FirstPersonController>();
		weaponAndAmmoManager = GetComponentInParent<WeaponAndAmmoManager>();
		trapControl = GetComponent<TrapControl>();
		laserControl = GetComponent<LaserControl>();
		grenadeControl = GetComponent<GrenadeControl>();
		knifeControl = GetComponent<KnifeControl>();
		cameragunControl = GetComponent<CameragunControl>();
		playerCanvasUpdater = GetComponentInParent<PlayerCanvasUpdater>();
		// trapControl.enabled = false;
		// laserControl.enabled = false;
		// grenadeControl.enabled = false;
		// knifeControl.enabled = false;
	}

	void Update(){
		if(GetComponentInParent<PlayerTimeManager>().playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
			if(playerIdentifier.myPlayerNum == 0){
				SelectLaser("DpadX");
				SelectKnife("DpadX");
				SelectGrenade("DpadY");
				SelectTrap("DpadY");
				SelectCamera(p1_selectCameraKey);
			} else if (playerIdentifier.myPlayerNum == 1){
				SelectLaser("P2_DpadX");
				SelectKnife("P2_DpadX");
				SelectGrenade("P2_DpadY");
				SelectTrap("P2_DpadY");
				SelectCamera(p2_selectCameraKey);
			}
		}
	}
	
	public void SelectCamera(KeyCode key){
		if(Input.GetKeyDown(key)){
			cameragunControl.enabled = true;
			cameragunControl.ActivateCameraOnWeaponSwitch();
			cameragunControl.firstPersonModel.SetActive(true);
			if(cameragunControl.cameraCount == 0){
				fpc.ToggleCameragunActive();
			}

			
			trapControl.enabled = false;
			laserControl.enabled = false;
			grenadeControl.enabled = false;
			knifeControl.enabled = false;
			trapControl.trap.SetActive(false);
			laserControl.laserPistol.SetActive(false);
			knifeControl.knife.SetActive(false);
			grenadeControl.firstPersonModel.SetActive(false);
		}
		//either select the camera launcher 
		//or switch to camera view. maybe you only have one?
	}
	public void SelectLaser(string _axis){
		if(Input.GetAxisRaw(_axis) == 1f && weaponAndAmmoManager.hasLaser){
			trapControl.enabled = false;
			laserControl.enabled = true;
			grenadeControl.enabled = false;
			knifeControl.enabled = false;
			trapControl.trap.SetActive(false);
			laserControl.laserPistol.SetActive(true);
			knifeControl.knife.SetActive(false);
			grenadeControl.firstPersonModel.SetActive(false);
			
			fpc.DeactivateCameraGun();
			cameragunControl.DeactivateCameraOnWeaponSwitch();
			cameragunControl.enabled = false;
			cameragunControl.firstPersonModel.SetActive(false);
		}
	}

	public void SelectKnife(string _axis){
		if(Input.GetAxisRaw(_axis) == -1f){
			trapControl.enabled = false;
			laserControl.enabled = false;
			grenadeControl.enabled = false;
			knifeControl.enabled = true;
			trapControl.trap.SetActive(false);
			laserControl.laserPistol.SetActive(false);
			grenadeControl.firstPersonModel.SetActive(false);
			knifeControl.knife.SetActive(true);

			fpc.DeactivateCameraGun();
			cameragunControl.DeactivateCameraOnWeaponSwitch();
			cameragunControl.enabled = false;
			cameragunControl.firstPersonModel.SetActive(false);
		}
	}

	public void SelectGrenade(string _axis){
 		if(Input.GetAxisRaw(_axis) == 1f && weaponAndAmmoManager.hasGrenade){
			trapControl.enabled = false;
			laserControl.enabled = false;
			grenadeControl.enabled = true;
			knifeControl.enabled = false;
			trapControl.trap.SetActive(false);
			laserControl.laserPistol.SetActive(false);
			knifeControl.knife.SetActive(false);
			grenadeControl.firstPersonModel.SetActive(true);
			grenadeControl.cooldown = 0;

			fpc.DeactivateCameraGun();
			cameragunControl.DeactivateCameraOnWeaponSwitch();
			cameragunControl.enabled = false;
			cameragunControl.firstPersonModel.SetActive(false);
		}
	}

	public void SelectTrap(string _axis){
		if(Input.GetAxisRaw(_axis) == -1f && weaponAndAmmoManager.hasTrap){
			trapControl.enabled = true;
			laserControl.enabled = false;
			grenadeControl.enabled = false;
			knifeControl.enabled = false;
			trapControl.trap.SetActive(true);
			laserControl.laserPistol.SetActive(false);
			knifeControl.knife.SetActive(false);
			grenadeControl.firstPersonModel.SetActive(false);
			trapControl.cooldown = 0;

			fpc.DeactivateCameraGun();
			cameragunControl.DeactivateCameraOnWeaponSwitch();
			cameragunControl.enabled = false;
			cameragunControl.firstPersonModel.SetActive(false);
		}
	}

	public void ResetWeaponCooldowns(){
		if(trapControl != null && grenadeControl != null){
			trapControl.cooldown = 0;
			grenadeControl.cooldown = 0;
		}
	}

	

/*	public void SelectTrap(KeyCode key){
		if(Input.GetKeyDown(key) && weaponAndAmmoManager.hasTrap){
			trapControl.enabled = true;
			laserControl.enabled = false;
			grenadeControl.enabled = false;
			knifeControl.enabled = false;
			trapControl.trap.SetActive(true);
			laserControl.laserPistol.SetActive(false);
			knifeControl.knife.SetActive(false);
			grenadeControl.grenade.SetActive(false);
			trapControl.cooldown = 0;
		}
	}*/

/*	public void SelectGrenade(KeyCode key){
		if(Input.GetKeyDown(key) && weaponAndAmmoManager.hasGrenade){
			trapControl.enabled = false;
			laserControl.enabled = false;
			grenadeControl.enabled = true;
			knifeControl.enabled = false;
			trapControl.trap.SetActive(false);
			laserControl.laserPistol.SetActive(false);
			knifeControl.knife.SetActive(false);
			grenadeControl.grenade.SetActive(true);
			grenadeControl.cooldown = 0;
		}
	}*/

	// public void SelectLaser(KeyCode key){
	// 	if(Input.GetKeyDown(key) && weaponAndAmmoManager.hasLaser){
	// 		trapControl.enabled = false;
	// 		laserControl.enabled = true;
	// 		grenadeControl.enabled = false;
	// 		knifeControl.enabled = false;
	// 		trapControl.trap.SetActive(false);
	// 		laserControl.laserPistol.SetActive(true);
	// 		knifeControl.knife.SetActive(false);
	// 		grenadeControl.grenade.SetActive(false);
	// 	}
	// }

	/*public void SelectKnife(KeyCode key){
		if(Input.GetKeyDown(key)){
			trapControl.enabled = false;
			laserControl.enabled = false;
			grenadeControl.enabled = false;
			knifeControl.enabled = true;
			trapControl.trap.SetActive(false);
			laserControl.laserPistol.SetActive(false);
			grenadeControl.grenade.SetActive(false);
			knifeControl.knife.SetActive(true);
		}
	}*/
	

}
