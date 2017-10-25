using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEngine : MonoBehaviour {

	Rigidbody rb;
	private float radius = 3F;
    private float power = 20.0F;	
	private int damage;

	private float speed;
	private float upwardsMod = 0f;
	void Awake(){
	
		rb = GetComponent<Rigidbody>();
		speed = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].speed;
		damage = Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].damage;
	}
	void Start () {
		
		MoveMortar();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
 	}

	public void MoveMortar(){
		rb.AddForce(transform.forward * speed, ForceMode.Impulse);
	}

	void OnCollisionEnter(){
		Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
			if(hit.tag == "Player" && hit.GetComponent<PlayerHealthManager>() != null){
 				Rigidbody rb = hit.GetComponent<Rigidbody>();
				hit.GetComponent<PlayerHealthManager>().DepleteHealth(damage);
				if (rb != null)
					// Debug.Log("hit " + rb.name);
					rb.AddExplosionForce(power, explosionPos, radius, upwardsMod, ForceMode.Impulse);
			}
        }

		Destroy(gameObject, 1.5f);
	}

	// void OnTriggerEnter(Collider coll){
	// 	if(coll.gameObject.tag == "Door"){
	// 		Destroy(gameObject);
	// 	}
	// }

}
