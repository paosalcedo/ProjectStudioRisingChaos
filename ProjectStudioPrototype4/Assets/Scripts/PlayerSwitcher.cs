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

	void OnTriggerExit(){
        GameObject otherPlayer = Instantiate(Services.Prefabs.Players[indexOfPlayerToSwitchTo]) as GameObject;
        otherPlayer.transform.position = startPos; 
        GameObject.Find("TrailGenerator").transform.SetParent(null);
        // gameObject.SetActive(false);
        StartCoroutine(SwitchToOtherPlayer(.01f));
    }


	IEnumerator SwitchToOtherPlayer(float delay){
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
		// GetComponentInChildren<Camera>().enabled = false;
		// GetComponent<CharacterController>().enabled = false;
		// GetComponent<FirstPersonController>().enabled = false;
		// GetComponent<AudioSource>().enabled = false;
	}
}
