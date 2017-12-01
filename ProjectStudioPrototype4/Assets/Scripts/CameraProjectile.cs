using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProjectile : MonoBehaviour {
	Rigidbody rb;
	private float radius = 3.5F;
    // private float power = 20.0F;	
	private int damage;
	private float speed;
	
	public virtual void Setup(WeaponType _weaponType){
		rb = GetComponent<Rigidbody>();
		speed = Services.WeaponDefinitions.weapons[_weaponType].speed;
		damage = Services.WeaponDefinitions.weapons[_weaponType].damage;
		Launch();
	}
	public virtual void Launch () {
		MoveProjectile();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
 	}

	public virtual void MoveProjectile(){
		rb.AddForce(transform.forward * speed, ForceMode.Impulse);
	}

}
