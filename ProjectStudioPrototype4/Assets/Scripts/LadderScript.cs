using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using DG.Tweening;
public class LadderScript : MonoBehaviour {
	private int myPlayerNum;

	PlayerIdentifier playerIdentifier;
	float ladderSpeed = 100f;
	// Use this for initialization
	Rigidbody rb;
	FirstPersonController fpc;
	void Start () {
		playerIdentifier = GetComponent<PlayerIdentifier>();
		fpc = GetComponent<FirstPersonController>();
		rb = GetComponent<Rigidbody>();
		myPlayerNum = playerIdentifier.myPlayerNum;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay(Collider coll){
		if(coll.gameObject.tag == "Ladder"){
			if(myPlayerNum == 0){
				if(Input.GetAxisRaw("Vertical") > 0){
					transform.Translate(Vector3.up * 0.05f);
				}
			} else {
				if(Input.GetAxisRaw("P2_Vertical") > 0){
					Debug.Log("P2 Going up a ladder!");
					transform.Translate(Vector3.up * 0.05f);
				}	
			}
		
		}
	}
}
