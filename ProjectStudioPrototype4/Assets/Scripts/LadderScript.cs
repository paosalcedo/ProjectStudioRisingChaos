using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour {
	private int myPlayerNum;
	float ladderSpeed = 1f;
	// Use this for initialization
	Rigidbody rb;
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider coll){
		if(coll.gameObject.tag == "Ladder"){
			Debug.Log("You are colliding with a ladder!");
			if(Input.GetAxisRaw("Vertical") > 0){
				Debug.Log("P1 Going up a ladder!");
				rb.AddForce(Vector3.up * ladderSpeed, ForceMode.Impulse);
			}
		
			if(Input.GetAxisRaw("P2_Vertical") > 0){
				Debug.Log("P2 Going up a ladder!");
				rb.AddForce(Vector3.up * ladderSpeed, ForceMode.Impulse);
			}
			
		}
	}
}
