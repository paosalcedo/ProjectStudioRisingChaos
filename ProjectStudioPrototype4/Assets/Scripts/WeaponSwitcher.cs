using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

	public enum SelectedWeapon{
		Laser,
		Grenade,
		Trap,
		Knife
	}

	private PlayerCanvasUpdater playerCanvasUpdater;
	public KeyCode selectTrapKey;
	public KeyCode selectLaserKey;
	public KeyCode selectGrenadeKey;
	public KeyCode selectKnifeKey;
	private LaserControl laserControl;
	private GrenadeControl grenadeControl;
	private TrapControl trapControl;
	private KnifeControl knifeControl;
	// Use this for initialization
	void Start () {
		trapControl = GetComponent<TrapControl>();
		laserControl = GetComponent<LaserControl>();
		grenadeControl = GetComponent<GrenadeControl>();
		knifeControl = GetComponent<KnifeControl>();
		playerCanvasUpdater = GetComponentInParent<PlayerCanvasUpdater>();
		// trapControl.enabled = false;
		// laserControl.enabled = false;
		// grenadeControl.enabled = false;
		// knifeControl.enabled = false;
	}

	void Update(){
		//only allow weapon switching if you're not frozen.
		if(GetComponentInParent<PlayerTimeManager>().playerFrozenState == PlayerTimeManager.PlayerFrozenState.Not_Frozen){
			SelectTrap(selectTrapKey);
			SelectGrenade(selectGrenadeKey);
			SelectKnife(selectKnifeKey);
			SelectLaser(selectLaserKey);
		}
	}
	
	public void SelectTrap(KeyCode key){
		if(Input.GetKeyDown(key)){
			trapControl.enabled = true;
			laserControl.enabled = false;
			grenadeControl.enabled = false;
			knifeControl.enabled = false;
			trapControl.trap.SetActive(true);
			laserControl.laserPistol.SetActive(false);
			knifeControl.knife.SetActive(false);
			grenadeControl.grenade.SetActive(false);
		}
	}

	public void SelectGrenade(KeyCode key){
		if(Input.GetKeyDown(key)){
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
	}

	public void SelectLaser(KeyCode key){
		if(Input.GetKeyDown(key)){
			trapControl.enabled = false;
			laserControl.enabled = true;
			grenadeControl.enabled = false;
			knifeControl.enabled = false;
			trapControl.trap.SetActive(false);
			laserControl.laserPistol.SetActive(true);
			knifeControl.knife.SetActive(false);
			grenadeControl.grenade.SetActive(false);
		}
	}

	public void SelectKnife(KeyCode key){
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
	}

}
