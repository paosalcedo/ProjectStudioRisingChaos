﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;
public class DestroyThis : MonoBehaviour {

	public InputField p1NameInputField;
	public InputField p2NameInputField;
	// Use this for initialization
	void Start () {
		GetComponent<Canvas>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			if(p1NameInputField.text != "" && p2NameInputField.text != "" && p1NameInputField.text != p2NameInputField.text){
				CurrentPlayerTracker.currentPlayer.GetComponent<PlayerTimeManager>().UnFreezeMe();
				PlayerNames.AssignNames(p1NameInputField.text, p2NameInputField.text);
				HideCursor();
 				StartCoroutine(LateDestroy(0.2f));
			}
		}
	}

	IEnumerator LateDestroy(float delay){
		yield return new WaitForSeconds(delay);
		GameObject.Find("CanvasP1").GetComponent<Canvas>().enabled = true;
		GameObject.Find("CanvasP2").GetComponent<Canvas>().enabled = true;
		Destroy(gameObject);
	}

	void HideCursor(){
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players){
			player.GetComponent<FirstPersonController>().HideCursor();
		}
	}
}
