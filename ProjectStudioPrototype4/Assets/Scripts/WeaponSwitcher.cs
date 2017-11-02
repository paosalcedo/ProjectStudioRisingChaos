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
		// trapControl.enabled = false;
		// laserControl.enabled = false;
		// grenadeControl.enabled = false;
		// knifeControl.enabled = false;
	}

	void Update(){

	}
	
	// Update is called once per frame
	public void SelectTrap(){
		trapControl.enabled = true;
		laserControl.enabled = false;
		grenadeControl.enabled = false;
		knifeControl.enabled = false;
	}

	public void SelectGrenade(){
		trapControl.enabled = true;
		laserControl.enabled = false;
		grenadeControl.enabled = true;
		knifeControl.enabled = false;
	}

	public void SelectLaser(){
		trapControl.enabled = false;
		laserControl.enabled = true;
		grenadeControl.enabled = false;
		knifeControl.enabled = false;
	}

	public void SelectKnife(){
		trapControl.enabled = false;
		laserControl.enabled = false;
		grenadeControl.enabled = false;
		knifeControl.enabled = true;
	}

}
