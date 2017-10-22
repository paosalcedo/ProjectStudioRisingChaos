using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour {

	
	private Vector3 startPos;
	public int indexOfPlayerToSwitchTo;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SwitchToOtherPlayer(float delay){
		yield return new WaitForSeconds(delay);
		// GetComponentInChildren<Camera>().enabled = false;
		// GetComponent<CharacterController>().enabled = false;
		// GetComponent<FirstPersonController>().enabled = false;
		// GetComponent<AudioSource>().enabled = false;
	}
}
