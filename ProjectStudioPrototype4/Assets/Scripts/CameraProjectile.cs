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
		// Launch();
	}

	void Start(){
		MoveProjectile();
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

	void OnCollisionEnter(Collision collision){
		// stick with whatever you collide with
		if(collision.collider.tag == "Player"){
			transform.SetParent(collision.transform);
			transform.localEulerAngles = new Vector3 (0, 0, 0);
		}
		rb.velocity = Vector3.zero;
		rb.isKinematic = true;
		transform.eulerAngles = new Vector3 (0, -180, 0);
		GetComponentInChildren<MouseLook>().enabled = true;
	}

	IEnumerator SetCameraMouseLookActive(float delay){
		yield return new WaitForSeconds(delay);
		GetComponentInChildren<MouseLook>().enabled = true;
	}

}
