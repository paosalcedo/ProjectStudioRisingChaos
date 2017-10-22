using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("EnableCollider", 5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnableCollider(){
		GetComponent<Collider>().enabled = true;
	}
}
